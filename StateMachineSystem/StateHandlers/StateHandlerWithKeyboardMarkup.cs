using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

public abstract class StateHandlerWithKeyboardMarkup : AbstractStateHandler
{
    private const string PIN_SYMBOL = "📍";
    
    // public abstract BotState NextState { get; }

    protected static ReplyMarkupBase GetReplyKeyboard(IEnumerable<IEnumerable<string>> buttons,
        bool resizeKeyboardMarkup = true)
    {
        var enumerable = buttons as IEnumerable<string>[] ?? buttons.ToArray();
        
        if (!enumerable.Any())
            return new ReplyKeyboardRemove();
        
        var replyKeyboardMarkup =
            new ReplyKeyboardMarkup(enumerable.Select(row =>
                row.Select(buttonText => new KeyboardButton(buttonText))))
            {
                ResizeKeyboard = resizeKeyboardMarkup
            };

        return replyKeyboardMarkup;
    }
    
    protected static ReplyMarkupBase GetReplyKeyboardWithCancel(IEnumerable<IEnumerable<string>> buttons,
        IStringLocalizer<Program> localizer,
        bool resizeKeyboardMarkup = true)
    {
        var replyKeyboardMarkup = GetReplyKeyboard(buttons, resizeKeyboardMarkup);

        if (replyKeyboardMarkup is ReplyKeyboardRemove)
            return replyKeyboardMarkup;

        ((ReplyKeyboardMarkup)replyKeyboardMarkup).Keyboard = ((ReplyKeyboardMarkup)replyKeyboardMarkup).Keyboard
            .Append(new[] { new KeyboardButton(localizer["Cancel"]) });

        return replyKeyboardMarkup;
    }

    protected static ReplyKeyboardMarkup GetReplyKeyboardWithLocation(IStringLocalizer<Program> localizer,
        bool resizeKeyboardMarkup = true)
    {
        var getLocationButton = KeyboardButton.WithRequestLocation($"{PIN_SYMBOL} {localizer[PIN_SYMBOL]}");
        var keyboard = new ReplyKeyboardMarkup(new[] { getLocationButton });
        keyboard.ResizeKeyboard = resizeKeyboardMarkup;

        return keyboard;
    }
}