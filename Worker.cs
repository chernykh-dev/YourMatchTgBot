using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly UserService _userService;
    private readonly InterestService _interestService;

    private readonly TelegramBotClient _telegramBotClient =
        new TelegramBotClient("6840671790:AAFa-HhMJZXiNL7KLqY1enC4A87rUOj_w-g");

    public Worker(ILogger<Worker> logger, UserService userService, InterestService interestService)
    {
        _logger = logger;
        _userService = userService;
        _interestService = interestService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var me = await _telegramBotClient.GetMeAsync(cancellationToken: stoppingToken);

        _telegramBotClient.StartReceiving(HandleUpdate, HandleError, cancellationToken: stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;
        // Only process text messages
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        var user = _userService.GetUserById(chatId);
        if (user == null)
        {
            // Заполнение анкеты. По сути команда `/start`

            user = new User
            {
                Id = chatId,
                Name = message.From.FirstName,
                Age = 23,
                City = "Белгород",
                Description = (await botClient.GetChatAsync(chatId, cancellationToken: cancellationToken)).Bio ?? "",
                ZodiacSign = "Дева",
                Height = 175,
                Education = "Высшее",
                Interests = _interestService.GetInterestsByIds(5, 6, 7)
            };

            // После заполнения выбор действий: просмотр анкет или настройка анкеты и поиска.
        }

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
    }

    private async Task HandleError(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {

    }
}