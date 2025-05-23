using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

public abstract class StateHandlerWithKeyboardMarkup : AbstractStateHandler
{
    private const string PIN_SYMBOL = "📍";
    protected const string SEARCH_SYMBOL = "\ud83d\udd0e";
    protected const string LIKE_SYMBOL = "\u2764\ufe0f";
    protected const string DISLIKE_SYMBOL = "\u274c";
    protected const string PROFILE_SYMBOL = "\ud83d\udc64";

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

    protected static ReplyMarkupBase GetLikesReplyKeyboard(bool resizeKeyboardMarkup = true)
    {
        return GetReplyKeyboard(new[] { new [] { DISLIKE_SYMBOL, PROFILE_SYMBOL, LIKE_SYMBOL } }, resizeKeyboardMarkup);
    }
}