using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace YourMatchTgBot.StateMachine.StateHandlers.Register;

[StateHandler(State.Register_WaitingForName)]
public class WaitingForNameHandler : StateHandlerWithCancel
{
    private readonly ILogger<WaitingForNameHandler> _logger;

    public WaitingForNameHandler(ILogger<WaitingForNameHandler> logger) 
        : base(new List<IEnumerable<string>>())
    {
        _logger = logger;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken)
    {
        var expectedName = update.Message.From.FirstName;

        var replyKeyboardMarkup = ReplyKeyboardMarkup.Keyboard.ToList();
        replyKeyboardMarkup.Insert(0, new [] { new KeyboardButton(expectedName) });
        ReplyKeyboardMarkup.Keyboard = replyKeyboardMarkup;

        // Можно вынести отправку text message.
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Name:", replyMarkup: ReplyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken)
    {
        var userName = update.Message.Text;
        
        _logger.LogInformation(userName);
        
        stateMachine.SetState(State.Register_WaitingForAge);
    }
}