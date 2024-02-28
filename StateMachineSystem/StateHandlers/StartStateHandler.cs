using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

[StateHandler(BotState.Start)]
public class StartStateHandler : IStateHandler
{
    private ILogger<StartStateHandler> _logger;

    public StartStateHandler(ILogger<StartStateHandler> logger)
    {
        _logger = logger;
    }

    public async Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        
    }

    public async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;

        if (update.Message.Text is not { } messageText)
            return;

        if (messageText != "/start")
            return;

        var chatId = message.Chat.Id;
        
        _logger.LogInformation("User {{id:{chatId}}} started", chatId);

        user.State = BotState.Register_WaitingForDescription;
    }
}