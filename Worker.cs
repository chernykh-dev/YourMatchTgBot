using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using YourMatchTgBot.StateMachineSystem;
using YourMatchTgBot.StateMachineSystem.StateHandlers;
using YourMatchTgBot.StateMachineSystem.StateHandlers.Register;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly UserService _userService;
    private readonly InterestService _interestService;

    private readonly ITelegramBotClient _telegramBotClient;

    private readonly StateMachine _stateMachine;

    private User _newUser = null;
    private string _lastBotRequest = null;
    private Update _lastUpdate = null;

    public Worker(ILogger<Worker> logger, UserService userService, InterestService interestService,
        StateMachine stateMachine, ITelegramBotClient telegramBotClient)
    {
        _logger = logger;
        _userService = userService;
        _interestService = interestService;
        _stateMachine = stateMachine;
        _telegramBotClient = telegramBotClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _telegramBotClient.StartReceiving(_stateMachine.ActivateCurrentState, HandleError, cancellationToken: stoppingToken);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }

        await _telegramBotClient.CloseAsync(cancellationToken: stoppingToken);
    }
    
    private async Task HandleError(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception.Message);
    }
}