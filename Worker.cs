using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.ReflectionExtensions;
using YourMatchTgBot.Services;
using YourMatchTgBot.StateMachineSystem;
using YourMatchTgBot.StateMachineSystem.StateHandlers;
using YourMatchTgBot.StateMachineSystem.StateHandlers.Register;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IUserService _userService;
    private readonly ApplicationDbContext _context;

    private readonly ITelegramBotClient _telegramBotClient;

    private readonly StateMachine _stateMachine;

    public Worker(ILogger<Worker> logger, IUserService userService,
        StateMachine stateMachine, ITelegramBotClient telegramBotClient, ApplicationDbContext context)
    {
        _logger = logger;
        _userService = userService;
        _stateMachine = stateMachine;
        _telegramBotClient = telegramBotClient;
        _context = context;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _telegramBotClient.StartReceiving(HandleUpdate, HandleError, cancellationToken: stoppingToken);
        
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

    private async Task HandleUpdate(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        var userId = update.GetUserId();
        var user = _userService.GetUserById(userId);
        
        if (user == null)
        {
            user = new User
            {
                Id = userId,
                State = StateMachine.InitialState
            };

            _userService.AddUser(user);
        }

        await _stateMachine.ActivateState(botClient, update, user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
    
    private async Task HandleError(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception.Message);

        await botClient.SendTextMessageAsync(472106852L, exception.Message, cancellationToken: cancellationToken);
    }
}