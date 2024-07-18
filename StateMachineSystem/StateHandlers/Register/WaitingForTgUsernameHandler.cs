using Microsoft.Extensions.Localization;
using NTextCat.Commons;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForTgUsername, BotState.Register_WaitingForName, MessageType.Text)]
public class WaitingForTgUsernameHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForTgUsernameHandler(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var chatInfo = await botClient.GetChatAsync(user.Id, cancellationToken: cancellationToken);

        if (!chatInfo.Username.IsNullOrEmpty())
        {
            ChangeState(user);

            return;
        }

        var keyboardButtons = new List<List<string>> { new() { _localizer["Continue"] } };

        var replyKeyboardMarkup = GetReplyKeyboard(keyboardButtons);

        await botClient.SendTextMessageAsync(update.Message.Chat, _localizer["NeedToSetUsername"],
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        if (update.Message.Text != _localizer["Continue"])
            return;

        var chatInfo = await botClient.GetChatAsync(user.Id, cancellationToken: cancellationToken);

        if (chatInfo.Username.IsNullOrEmpty())
            return;

        ChangeState(user);
    }
}