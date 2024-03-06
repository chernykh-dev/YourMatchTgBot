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

    public WatchProfilesHandler(IUserService userService, UserProfileService userProfileService)
    {
        _userService = userService;
        _userProfileService = userProfileService;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var findedUser = _userService.FindUserForUser(user);

        if (findedUser == null)
        {
            await botClient.SendTextMessageAsync(user.Id,
                "Users not found", cancellationToken: cancellationToken);

            user.State = BotState.Register_ShowProfile;

            return;
        }
        
        var replyKeyboardMarkup = GetReplyKeyboard(new[] { new string[] { "Ok" }, new string[] { "Not Ok" } });
        
        await botClient.SendTextMessageAsync(user.Id,
            "Loopa",
            replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);

        var album = await _userProfileService.GetUserProfileMessage(findedUser, cancellationToken, user.LanguageCode);

        await botClient.SendMediaGroupAsync(user.Id, album, cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        
    }
}