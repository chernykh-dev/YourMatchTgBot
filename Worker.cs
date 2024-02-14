using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using YourMatchTgBot.StateMachine;
using YourMatchTgBot.StateMachine.StateHandlers;
using YourMatchTgBot.StateMachine.StateHandlers.Register;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly UserService _userService;
    private readonly InterestService _interestService;

    private readonly TelegramBotClient _telegramBotClient =
        new TelegramBotClient("6840671790:AAFa-HhMJZXiNL7KLqY1enC4A87rUOj_w-g");

    private readonly StateMachine.StateMachine _stateMachine;

    private User _newUser = null;
    private string _lastBotRequest = null;
    private Update _lastUpdate = null;

    public Worker(ILogger<Worker> logger, UserService userService, InterestService interestService, StateMachine.StateMachine stateMachine)
    {
        _logger = logger;
        _userService = userService;
        _interestService = interestService;
        _stateMachine = stateMachine;

        /*
        _stateMachine.RegisterHandler(State.Start, new StartStateHandler(_userService));
        _stateMachine.RegisterHandler(State.Register_WaitingForName, new WaitingForNameHandler(logger));
        _stateMachine.RegisterHandler(State.Register_WaitingForAge, new WaitingForAgeHandler(logger));
        */
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

    private async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        /*
        var photos = await botClient.GetUserProfilePhotosAsync(chatId, cancellationToken: cancellationToken);

        var album = photos.Photos
            .Select(photo => new InputMediaPhoto(InputFile.FromFileId(photo[2].FileId)))
            .Cast<IAlbumInputMedia>().ToList();

        (album.First() as InputMedia).Caption = user.GetTextProfile();

        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new []
            {
                new KeyboardButton("\u2764"),
                new KeyboardButton("\u274c")
            }
        });

        // Отправляется только при начале просмотра анкет.
        // После просмотра анкет кастомную клавиатуру необходимо удалить.
        await botClient.SendTextMessageAsync(chatId, "...", replyMarkup: keyboard,
            cancellationToken: cancellationToken);

        var messages = await botClient.SendMediaGroupAsync(chatId, album, cancellationToken: cancellationToken);
        */
    }

    private async Task HandleError(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        
    }
}