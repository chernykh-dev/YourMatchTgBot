using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Geolocation;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourMatchTgBot.Models;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForLocation, BotState.Register_WaitingForPhotos, MessageType.Text, MessageType.Location)]
public class WaitingForLocationHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;
    private readonly ILogger<WaitingForLocationHandler> _logger;

    private readonly HttpClient _httpClient = new HttpClient();

    private readonly ICityService _cityService;

    public WaitingForLocationHandler(IStringLocalizer<Program> localizer, ILogger<WaitingForLocationHandler> logger, ICityService cityService)
    {
        _localizer = localizer;
        _logger = logger;
        _cityService = cityService;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        if (user.City == null)
        {
            var replyKeyboardMarkup = GetReplyKeyboardWithLocation(_localizer);
        
            await botClient.SendTextMessageAsync(user.Id, _localizer["WaitingLocation"],
                replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);
        }
        else
        {
            var keyboardReply = GetReplyKeyboard(new[] { new string[] { _localizer["No"], _localizer["Yes"] } });

            var localizedCityName = user.City.DisplayName;
            if (user.LanguageCode == "ru")
            {
                localizedCityName = user.City.TranslatedDisplayName;
            }
            
            await botClient.SendTextMessageAsync(user.Id, string.Format(_localizer["CorrectCity"], localizedCityName),
                replyMarkup: keyboardReply,
                cancellationToken: cancellationToken);
        }
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        if (user.City != null)
        {
            if (update.Message?.Text == _localizer["Yes"])
            {
                ChangeState(user);

                return;
            }
            
            if (update.Message?.Text == _localizer["No"])
            {
                user.City = null;

                await botClient.SendTextMessageAsync(user.Id, _localizer["ConcreteLocation"],
                    cancellationToken: cancellationToken);

                return;
            }
        }
        
        City? city = null;
        if (update.Message?.Location is { } location)
        {
            _logger.LogInformation("{Latitude} {Longitude}", location.Latitude, location.Longitude);

            // var coordinate = new Coordinate(location.Latitude, location.Longitude);
            // var destination = new Coordinate(47.222078, 39.720358);

            // _logger.LogInformation("{Distance}",
            //      GeoCalculator.GetDistance(coordinate, destination, distanceUnit: DistanceUnit.Kilometers));
            // return;

            var result = await ParseJson(new Uri("http://nominatim.openstreetmap.org/reverse?format=json&lat="
                                                 + location.Latitude.ToString(CultureInfo.InvariantCulture)
                                                     .Replace(',', '.') + "&lon=" +
                                                 location.Longitude.ToString(CultureInfo.InvariantCulture)
                                                     .Replace(',', '.') +
                                                 "&zoom=10&format=jsonv2&accept-language=en"), cancellationToken);
                
            if (result is JObject obj)
            {
                var name = obj["name"].Value<string>();
                var displayName = obj["display_name"].Value<string>();

                city = new City
                {
                    Id = obj["place_id"].Value<long>(),
                    Name = name,
                    DisplayName = displayName,
                    TranslatedName = await TranslateService.TranslateText(name, "en", "ru", cancellationToken),
                    TranslatedDisplayName = await TranslateService.TranslateText(displayName, "en", "ru", cancellationToken)
                };

                user.Latitude = location.Latitude;
                user.Longitude = location.Longitude;
            }
        }
        else
        {
            var cityString = update.Message?.Text;

            var language = TextLanguageDetectionService.DetectLanguage(cityString);

            if (language is not ("en" or "ru"))
            {
                language = "en";
            }

            // https://nominatim.openstreetmap.org/search.php?q={city}&format=jsonv2&accept-language=en
            var result =
                await ParseJson(
                    new Uri(
                        $"https://nominatim.openstreetmap.org/search.php?city={cityString}&format=jsonv2&accept-language={language}"),
                    cancellationToken);

            if (result is JArray arr)
            {
                foreach (var obj in arr)
                {
                    // Вариант с областями.
                    /*
                    if (obj["type"].Value<string>() != "city" && obj["type"].Value<string>() != "town" && 
                        obj["type"].Value<string>() != "administrative")
                        continue;
                    */
                    
                    // Вариант без областей.

                    if (obj["type"]?.Value<string>() == "city" || obj["type"]?.Value<string>() == "town" ||
                        obj["type"]?.Value<string>() == "administrative" && obj["addresstype"]?.Value<string>() == "city")
                    {
                        var name = obj["name"].Value<string>();
                        var displayName = obj["display_name"].Value<string>();

                        city = new City
                        {
                            Id = obj["place_id"].Value<long>(),
                            Name = language == "en"
                                ? name : await TranslateService.TranslateText(name, "ru", "en", cancellationToken),
                            DisplayName = language == "en"
                                ? displayName : await TranslateService.TranslateText(displayName, "ru", "en", cancellationToken),
                            TranslatedName = language == "en"
                                ? await TranslateService.TranslateText(name, "en", "ru", cancellationToken) : name,
                            TranslatedDisplayName = language == "en"
                                ? await TranslateService.TranslateText(displayName, "en", "ru", cancellationToken) : displayName
                        };

                        user.Latitude = obj["lat"].Value<double>();
                        user.Longitude = obj["lon"].Value<double>();

                        break;
                    }
                }
            }

            if (city != null && (cityString == city.Name || 
                    cityString == await TranslateService.TranslateText(city.Name, "en", "ru", cancellationToken)))
            {
                var existingCityByString = _cityService.GetCity(city.Id);

                user.City = existingCityByString ?? _cityService.AddCity(city);
                
                ChangeState(user);

                return;
            }
        }

        if (city == null)
            return;

        var existingCity = _cityService.GetCity(city.Id);

        user.City = existingCity ?? _cityService.AddCity(city);

        _logger.LogInformation("{City}", city.Name);
    }
    
    private async Task<object> ParseJson(Uri uri, CancellationToken stoppingToken)
    {
        var request = new HttpRequestMessage();
        request.Headers.Add("user-agent",
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        request.Headers.Add("Referer", "http://www.microsoft.com");
        request.RequestUri = uri;

        var response = await _httpClient.SendAsync(request, stoppingToken);

        var json = await response.Content.ReadAsStringAsync(stoppingToken);

        return JsonConvert.DeserializeObject(json) ?? throw new InvalidOperationException();
    }
}