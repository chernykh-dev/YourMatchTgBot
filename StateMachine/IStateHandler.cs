using Telegram.Bot;
using Telegram.Bot.Types;

namespace YourMatchTgBot.StateMachine;

public interface IStateHandler
{
    public Task RequestToUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken);

    public Task ResponseFromUser(ITelegramBotClient botClient, Update update, StateMachine stateMachine,
        CancellationToken cancellationToken);
}