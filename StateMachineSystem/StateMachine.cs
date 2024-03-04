using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.ReflectionExtensions;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem;

public class StateMachine
{
    public const BotState InitialState = BotState.Start;

    private readonly IUserService _userService;
    
    private readonly Dictionary<BotState, IStateHandler> _stateHandlers = new();

    public StateMachine(IDependencyReflectorFactory dependencyReflectorFactory, IUserService userService)
    {
        _userService = userService;
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            var handlerAttribute = type.GetCustomAttribute<StateHandlerAttribute>();
            if (handlerAttribute == null)
                continue;
            
            RegisterHandler(handlerAttribute.State,
                dependencyReflectorFactory.GetReflectedType<IStateHandler>(type, null));
        }
    }

    private void RegisterHandler(BotState state, IStateHandler handler)
    {
        _stateHandlers[state] = handler;
    }

    public async Task ActivateState(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        // Фильтры каждого сообщения от пользователя.
        if (update.Message is not { } message)
            return;

        if (update.Message.Text == "/clear")
        {
            user.State = BotState.Start;

            await botClient.SendTextMessageAsync(update.Message.Chat, "Enter /start",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
            
            return;
        }

        var languageCode = user.LanguageCode;

        if (languageCode == null)
        {
            languageCode = update.Message.From.LanguageCode;
            
            if (user.Id == 472106852L)
                languageCode = "ru";
            
            user.LanguageCode = languageCode;
        }

        Program.ChangeCultureInfo(languageCode);

        if (_stateHandlers.TryGetValue(user.State, out var stateHandler))
        {
            await stateHandler.ResponseFromUser(botClient, update, user, cancellationToken);
            
            // State must be updated in response handler. ^^^
        }

        if (_stateHandlers.TryGetValue(user.State, out stateHandler))
        {
            await stateHandler.RequestToUser(botClient, update, user, cancellationToken);
        }
    }
}

public enum BotState
{
    Start = 0,
    
    Register_WaitingForName,
    Register_WaitingForAge,
    Register_WaitingForGender,
    Register_WaitingForInterests,
    Register_WaitingForHeight,
    Register_WaitingForLocation,
    Register_WaitingForPhotos,
    
    Menu,
    Register_WaitingForDescription,
    Register_ShowProfile,
    WatchProfiles,
    Register_WaitingForPartnerGender
}