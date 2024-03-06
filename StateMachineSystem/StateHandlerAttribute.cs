using Telegram.Bot.Types.Enums;

namespace YourMatchTgBot.StateMachineSystem;

public class StateHandlerAttribute : Attribute
{
    public BotState State { get; }
    
    public BotState NextState { get; }
    
    public MessageType[] AllowedMessageTypes { get; }
    
    public StateHandlerAttribute(BotState state, BotState nextState, params MessageType[] allowedMessageTypes)
    {
        State = state;
        NextState = nextState;
        AllowedMessageTypes = allowedMessageTypes;
    }
}