using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

[StateHandler(BotState.PreWatchProfiles, BotState.WatchProfiles, MessageType.Text)]
public class PreWatchProfilesHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;

    public PreWatchProfilesHandler(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var replyKeyboardMarkup = GetLikesReplyKeyboard();

        await botClient.SendTextMessageAsync(user.Id,
            _localizer["\ud83d\udd0d"],
            replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);

        ChangeState(user);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {

    }
}