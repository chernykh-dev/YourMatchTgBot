namespace YourMatchTgBot.StateMachine;

public class StateHandlerAttribute : Attribute
{
    public State State { get; }
    
    public StateHandlerAttribute(State state)
    {
        State = state;
    }
}