using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForGender)]
public class WaitingForGender : IStateHandler
{
    private readonly ILogger<WaitingForGender> _logger;

    public WaitingForGender(ILogger<WaitingForGender> logger)
    {
        _logger = logger;
    }

    public async Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var reply = new ReplyKeyboardMarkup(new[] { new KeyboardButton("Man"), new KeyboardButton("Women") });
        reply.ResizeKeyboard = true;
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Gender:", replyMarkup: reply,
            cancellationToken: cancellationToken);
    }

    public async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        if (userInput is not "Man" and "Women")
        {
            return;
        }
        
        // Interests.

        user.State = BotState.Register_WaitingForInterests;
    }
}