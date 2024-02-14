using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace YourMatchTgBot;

public class GetNameReceiver : IUpdateHandler
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var expectedName = update.Message.From.FirstName;

        var replyKeyboardMarkup = new ReplyKeyboardMarkup(new KeyboardButton(expectedName));
        replyKeyboardMarkup.ResizeKeyboard = true;

        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Name:", replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);

        var nameUpdates = await botClient.GetUpdatesAsync(limit: 1, cancellationToken: cancellationToken);
        
        Console.WriteLine(nameUpdates[0].Message.Text);
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        throw exception;
    }
}