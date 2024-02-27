namespace YourMatchTgBot.StateMachineSystem;

public class StateHandlerAttribute : Attribute
{
    public BotState State { get; }
    
    public StateHandlerAttribute(BotState state)
    {
        State = state;
    }
}