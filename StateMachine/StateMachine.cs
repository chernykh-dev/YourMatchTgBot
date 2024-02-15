using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using YourMatchTgBot.ReflectionExtensions;

namespace YourMatchTgBot.StateMachine;

public class StateMachine
{
    private readonly State _initialState;
    private readonly Dictionary<State, IStateHandler> _stateHandlers = new();

    public State State { get; private set; }

    public StateMachine(State initialState, IDependencyReflectorFactory dependencyReflectorFactory)
    {
        _initialState = initialState;
        State = initialState;

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            var handlerAttribute = type.GetCustomAttribute<StateHandlerAttribute>();
            if (handlerAttribute != null)
            {
                var ctorParameters = type.GetConstructors()[0].GetParameters();
                
                RegisterHandler(handlerAttribute.State, dependencyReflectorFactory.GetReflectedType<IStateHandler>(type, null));
            }
        }
    }

    public void RegisterHandler(State state, IStateHandler handler)
    {
        _stateHandlers[state] = handler;
    }

    public async Task ActivateCurrentState(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
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
    
    public void SetState(State state)
    {
        State = state;
    }

    public void Reset()
    {
        State = _initialState;
    }
}

public enum State
{
    Start = 0,
    
    Register_WaitingForName,
    Register_WaitingForAge,
    Register_WaitingForGender,
    Register_WaitingForInterests,
    
    Menu,
    
    
}