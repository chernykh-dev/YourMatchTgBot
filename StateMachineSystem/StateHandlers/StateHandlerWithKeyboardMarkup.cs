using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

public abstract class StateHandlerWithKeyboardMarkup : IStateHandler
{
    public abstract Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken);

    public abstract Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken);
    
    protected static ReplyKeyboardMarkup GetReplyKeyboard(IEnumerable<IEnumerable<string>> buttons,
        bool resizeKeyboardMarkup = true)
    {
        var replyKeyboardMarkup =
            new ReplyKeyboardMarkup(buttons.Select(row =>
                row.Select(buttonText => new KeyboardButton(buttonText))))
            {
                ResizeKeyboard = resizeKeyboardMarkup
            };

        return replyKeyboardMarkup;
    }
    
    protected static ReplyKeyboardMarkup GetReplyKeyboardWithCancel(IEnumerable<IEnumerable<string>> buttons,
        IStringLocalizer<Program> localizer,
        bool resizeKeyboardMarkup = true)
    {
        var replyKeyboardMarkup = GetReplyKeyboard(buttons, resizeKeyboardMarkup);

        replyKeyboardMarkup.Keyboard = replyKeyboardMarkup.Keyboard
            .Append(new[] { new KeyboardButton(localizer["Cancel"]) });

        return replyKeyboardMarkup;
    }
}