using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace YourMatchTgBot.StateMachine.StateHandlers.Register;

[StateHandler(State.Register_WaitingForGender)]
public class WaitingForGender : IStateHandler
{
    private readonly ILogger<WaitingForGender> _logger;

    public WaitingForGender(ILogger<WaitingForGender> logger)
    {
        _logger = logger;
    }

    public async Task RequestToUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken)
    {
        
    }

    public async Task ResponseFromUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        if (userInput == "Man" || userInput == "Women")
        {
            // Interests.
            stateMachine.SetState(State.Register_WaitingForInterests);
            return;
        }
        
        var reply = new ReplyKeyboardMarkup(new[] { new KeyboardButton("Man"), new KeyboardButton("Women") });
        reply.ResizeKeyboard = true;
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Gender:", replyMarkup: reply,
            cancellationToken: cancellationToken);
        return;
    }
}