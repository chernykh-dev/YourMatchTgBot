using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace YourMatchTgBot.StateMachine.StateHandlers;

public abstract class StateHandlerWithReplyKeyboard : IStateHandler
{
    protected readonly IEnumerable<IEnumerable<string>> VariantsKeyboard;

    protected readonly ReplyKeyboardMarkup ReplyKeyboardMarkup;

    protected StateHandlerWithReplyKeyboard(IEnumerable<IEnumerable<string>> variants, bool resizeKeyboardMarkup = true)
    {
        var variantsList = variants.ToList();
        VariantsKeyboard = variantsList;
        ReplyKeyboardMarkup = new ReplyKeyboardMarkup(variantsList
            .Select(vRow => vRow
                .Select(v => new KeyboardButton(v)))
        )
        {
            ResizeKeyboard = resizeKeyboardMarkup
        };
    }

    public abstract Task RequestToUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken);

    public abstract Task ResponseFromUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken);
}