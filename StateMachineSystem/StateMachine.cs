using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using YourMatchTgBot.ReflectionExtensions;

namespace YourMatchTgBot.StateMachineSystem;

public class StateMachine
{
    private readonly BotState _initialState;
    private readonly Dictionary<BotState, IStateHandler> _stateHandlers = new();

    public BotState State { get; private set; }

    public StateMachine(BotState initialState, IDependencyReflectorFactory dependencyReflectorFactory)
    {
        _initialState = initialState;
        State = initialState;

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            var handlerAttribute = type.GetCustomAttribute<StateHandlerAttribute>();
            if (handlerAttribute == null)
                continue;
            
            // TODO: Delete.
            var ctorParameters = type.GetConstructors()[0].GetParameters();

            RegisterHandler(handlerAttribute.State,
                dependencyReflectorFactory.GetReflectedType<IStateHandler>(type, null));
        }
    }

    public void RegisterHandler(BotState state, IStateHandler handler)
    {
        _stateHandlers[state] = handler;
    }

    public async Task ActivateCurrentState(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Фильтры каждого сообщения от пользователя.
        if (update.Message is not { } message || message.Chat.Id != 472106852L)
            return;
        
        if (_stateHandlers.TryGetValue(State, out var stateHandler))
        {
            // await stateHandler.RequestToUser(botClient, update, this, cancellationToken);
            
            await stateHandler.ResponseFromUser(botClient, update, this, cancellationToken);
            
            // State must be updated in response handler. ^^^
        }
        
        if (_stateHandlers.TryGetValue(State, out stateHandler))
        {
            await stateHandler.RequestToUser(botClient, update, this, cancellationToken);
        }
    }
    
    public void SetState(BotState state)
    {
        State = state;
    }

    public void Reset()
    {
        State = _initialState;
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