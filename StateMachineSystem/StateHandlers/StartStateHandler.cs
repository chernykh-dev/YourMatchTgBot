using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.Services;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

[StateHandler(BotState.Start)]
public class StartStateHandler : IStateHandler
{
    private readonly UserService _userService;
    private ILogger<StartStateHandler> _logger;
    // private StateMachine _stateMachine;

    public StartStateHandler(UserService userService, ILogger<StartStateHandler> logger/*, StateMachine stateMachine*/)
    {
        _userService = userService;
        _logger = logger;
        // _stateMachine = stateMachine;
    }

    public async Task RequestToUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken)
    {
        
    }

    public async Task ResponseFromUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
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

        var user = _userService.GetUserById(chatId);
        if (user == null)
        {
            stateMachine.SetState(BotState.Register_WaitingForName);
            return;

            /*
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
            */
            // После заполнения выбор действий: просмотр анкет или настройка анкеты и поиска.
        }

        stateMachine.SetState(BotState.Menu);
    }
}