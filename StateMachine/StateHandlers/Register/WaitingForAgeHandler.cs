using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace YourMatchTgBot.StateMachine.StateHandlers.Register;

[StateHandler(State.Register_WaitingForAge)]
public class WaitingForAgeHandler : IStateHandler
{
    private readonly ILogger<WaitingForAgeHandler> _logger;

    public WaitingForAgeHandler(ILogger<WaitingForAgeHandler> logger)
    {
        _logger = logger;
    }

    public async Task RequestToUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Age or birthday:", replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }

    public async Task ResponseFromUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        uint userAge;
        if (DateTime.TryParseExact(userInput, "dd.MM.yyyy", null, DateTimeStyles.None, out var userBirthDate))
        {
            userAge = (uint)(DateTime.Now.Year - userBirthDate.Year);
        }
        else if (!uint.TryParse(userInput, out userAge))
        {
            await RequestToUser(botClient, update, stateMachine, cancellationToken);
            return;
        }
        
        _logger.LogInformation(userAge.ToString());

        /*
        var reply = new ReplyKeyboardMarkup(new[] { new KeyboardButton("Man"), new KeyboardButton("Women") });
        reply.ResizeKeyboard = true;
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Gender:", replyMarkup: reply,
            cancellationToken: cancellationToken);
        */
        stateMachine.SetState(State.Register_WaitingForGender);
    }
}