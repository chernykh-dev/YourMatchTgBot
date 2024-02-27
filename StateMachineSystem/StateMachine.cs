using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using YourMatchTgBot.ReflectionExtensions;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem;

public class StateMachine
{
    public const BotState InitialState = BotState.Start;

    private readonly Dictionary<BotState, IStateHandler> _stateHandlers = new();

    public StateMachine(IDependencyReflectorFactory dependencyReflectorFactory)
    {
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
        
        if (_stateHandlers.TryGetValue(State, out var stateHandler))
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
    
    Menu,
    
    
}