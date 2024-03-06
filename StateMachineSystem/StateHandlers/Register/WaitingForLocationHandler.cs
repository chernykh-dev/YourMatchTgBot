using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Geolocation;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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

            var localizedCityName = user.City.Name;
            if (user.LanguageCode == "ru")
            {
                localizedCityName =
                    await TranslateService.TranslateText(localizedCityName, "en", "ru", cancellationToken);
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

                return;
            }
        }
        
        string? cityName = null;
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
                cityName = obj["address"]["city"].Value<string>();
            }
        }
        else
        {
            var cityString = update.Message?.Text;
            
            // https://nominatim.openstreetmap.org/search.php?q={city}&format=jsonv2&accept-language=en
            var result =
                await ParseJson(
                    new Uri(
                        $"https://nominatim.openstreetmap.org/search.php?q={cityString}&format=jsonv2&accept-language=en"),
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
                        cityName = obj["name"]?.Value<string>();
                        break;
                    }
                }
            }

            if (cityName != null && (cityString == cityName || 
                    cityString == await TranslateService.TranslateText(cityName, "en", "ru", cancellationToken)))
            {
                var existingCityByString = _cityService.GetCityByName(cityName);

                user.City = existingCityByString ?? _cityService.AddCity(cityName);
                
                ChangeState(user);

                return;
            }
        }

        if (cityName == null)
            return;

        var existingCity = _cityService.GetCityByName(cityName);

        user.City = existingCity ?? _cityService.AddCity(cityName);

        _logger.LogInformation("{City}", cityName);
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