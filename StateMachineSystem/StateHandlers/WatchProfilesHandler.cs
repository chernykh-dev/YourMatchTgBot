using Geolocation;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

[StateHandler(BotState.WatchProfiles, BotState.Register_ShowProfile, MessageType.Text /* TODO: Возможно + стикеры */)]
public class WatchProfilesHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IUserService _userService;
    private readonly UserProfileService _userProfileService;
    private readonly UserMatchingService _userMatchingService;
    private readonly ILogger<WatchProfilesHandler> _logger;

    public WatchProfilesHandler(IUserService userService, UserProfileService userProfileService, UserMatchingService userMatchingService, ILogger<WatchProfilesHandler> logger)
    {
        _userService = userService;
        _userProfileService = userProfileService;
        _userMatchingService = userMatchingService;
        _logger = logger;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var foundUsers = _userMatchingService.MatchForUserByDistance(user);

        if (foundUsers.Count == 0)
        {
            await botClient.SendTextMessageAsync(user.Id,
                "Users not found", cancellationToken: cancellationToken);

            user.State = BotState.Register_ShowProfile;

            return;
        }

        var foundUser = foundUsers[user.CurrentOffset++];

        var prob = UserMatchingService.UsersProbability(user, foundUser);

        _logger.LogInformation("Prob: {prob}, Distance: {distance}",
            prob,
            GeoCalculator.GetDistance(user.Latitude.Value, user.Longitude.Value, foundUser.Latitude.Value,
                foundUser.Longitude.Value, 1, DistanceUnit.Kilometers));

        var replyKeyboardMarkup = GetReplyKeyboard(new[] { new string[] { "Ok" }, new string[] { "Not Ok" } });

        await botClient.SendTextMessageAsync(user.Id,
            "Loopa",
            replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);

        var album = await _userProfileService.GetUserProfileMessage(foundUser, cancellationToken, user.LanguageCode);

        await botClient.SendMediaGroupAsync(user.Id, album, cancellationToken: cancellationToken);

        // Обрабатываем последнего пользователя в серии.
        if (user.CurrentOffset == foundUsers.Count)
        {
            user.CurrentOffset = 0;

            // Последняя серия поиска.
            if (foundUsers.Count < UserMatchingService.USERS_BY_SEARCH)
            {
                user.SearchOffset = 0;
            }
            // Не последняя серия поиска.
            else
            {
                user.SearchOffset += UserMatchingService.USERS_BY_SEARCH;
            }
        }
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        
    }
}