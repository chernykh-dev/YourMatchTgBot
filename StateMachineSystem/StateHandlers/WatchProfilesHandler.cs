using Telegram.Bot;
using Telegram.Bot.Types;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

[StateHandler(BotState.WatchProfiles)]
public class WatchProfilesHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IUserService _userService;
    private readonly UserProfileService _userProfileService;

    public WatchProfilesHandler(IUserService userService, UserProfileService userProfileService)
    {
        _userService = userService;
        _userProfileService = userProfileService;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var findedUser = _userService.FindUserForUser(user);
        
        var replyKeyboardMarkup = GetReplyKeyboard(new[] { new string[] { "Ok" }, new string[] { "Not Ok" } });
        
        await botClient.SendTextMessageAsync(update.Message.Chat,
            "Loopa",
            replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);

        var album = _userProfileService.GetUserProfileMessage(findedUser);

        await botClient.SendMediaGroupAsync(update.Message.Chat, album, cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        
    }
}