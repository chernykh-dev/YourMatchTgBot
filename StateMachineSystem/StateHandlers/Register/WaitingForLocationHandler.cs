using System.Globalization;
using Geolocation;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForLocation)]
public class WaitingForLocationHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;
    private readonly ILogger<WaitingForLocationHandler> _logger;

    private readonly HttpClient _httpClient = new HttpClient();

    public WaitingForLocationHandler(IStringLocalizer<Program> localizer, ILogger<WaitingForLocationHandler> logger)
    {
        _localizer = localizer;
        _logger = logger;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var replyKeyboardMarkup = GetReplyKeyboardWithLocation(_localizer);
        
        await botClient.SendTextMessageAsync(update.Message.Chat, _localizer["WaitingLocation"],
            replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        string? city = null;
        if (update.Message.Location is { } location)
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
                                                 "&zoom=10&format=jsonv2"), cancellationToken);
                
            if (result is JObject obj)
            {
                city = obj["address"]["city"].Value<string>();
            }
        }
        else
        {
            if (update.Message.Text is not { } cityString)
                return;

            var result = await ParseJson(new Uri("https://nominatim.openstreetmap.org/search.php?q=" + cityString +
                                                 "&format=jsonv2"), cancellationToken);

            if (result is JArray arr)
            {
                foreach (var obj in arr)
                {
                    if (obj["type"].Value<string>() != "city" && obj["type"].Value<string>() != "town")
                        continue;
                    
                    city = obj["name"].Value<string>();
                    break;
                }
            }
        }

        if (city == null)
            throw new Exception();
            
        _logger.LogInformation("{City}", city);
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