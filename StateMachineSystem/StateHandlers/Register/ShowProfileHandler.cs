using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_ShowProfile)]
public class ShowProfileHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;
    private readonly UserProfileService _userProfileService;

    public ShowProfileHandler(IStringLocalizer<Program> localizer, UserProfileService userProfileService)
    {
        _localizer = localizer;
        _userProfileService = userProfileService;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var replyKeyboardMarkup = GetReplyKeyboard(new[] { new string[] { _localizer["Continue"] }, new string[] { _localizer["FillProfileAgain"] } });
        
        await botClient.SendTextMessageAsync(update.Message.Chat,
            _localizer["ReadyProfile"],
            replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);

        var album = _userProfileService.GetUserProfileMessage(user);

        await botClient.SendMediaGroupAsync(update.Message.Chat, album, cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        if (update.Message.Text == _localizer["Continue"])
        {
            user.State = BotState.WatchProfiles;
            return;
        }

        if (update.Message.Text == _localizer["FillProfileAgain"])
        {
            user.Interests.Clear();
            user.Photos.Clear();

            user.State = BotState.Register_WaitingForName;
            return;
        }
    }
}