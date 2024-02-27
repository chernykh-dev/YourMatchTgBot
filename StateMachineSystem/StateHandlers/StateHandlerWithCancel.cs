using Telegram.Bot;
using Telegram.Bot.Types;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

public abstract class StateHandlerWithCancel : StateHandlerWithReplyKeyboard
{
    protected StateHandlerWithCancel(IEnumerable<IEnumerable<string>> variants, bool resizeKeyboardMarkup = true) 
        : base(variants.Append(new [] { "Cancel" }), resizeKeyboardMarkup)
    {
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken)
    {
        if (update.Message.Text == "Cancel")
        {
            stateMachine.Reset();
            return;
        }
    }
}