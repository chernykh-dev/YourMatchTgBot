using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

[StateHandler(BotState.Start, BotState.Register_WaitingForLanguage, MessageType.Text)]
public class StartStateHandler : AbstractStateHandler
{
    private ILogger<StartStateHandler> _logger;

    // protected override BotState NextState => BotState.Register_ShowTermsOfUse;

    public StartStateHandler(ILogger<StartStateHandler> logger)
    {
        _logger = logger;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken)
    {
        if (update.Message.Text != "/start")
            return;

        _logger.LogInformation("User {{id:{userId}}} started", user.Id);
        
        ChangeState(user);
    }
}