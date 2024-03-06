using System.Reflection;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem;

public abstract class AbstractStateHandler
{
    // protected abstract BotState NextState { get; }
    
    public abstract Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken);

    public abstract Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken);

    public IEnumerable<MessageType> AllowedMessageTypes =>
        ((StateHandlerAttribute)GetType().GetCustomAttribute(typeof(StateHandlerAttribute))).AllowedMessageTypes;

    protected void ChangeState(User user)
    {
        var attribute = (StateHandlerAttribute)GetType().GetCustomAttribute(typeof(StateHandlerAttribute));

        user.State = attribute.NextState;
    }
}