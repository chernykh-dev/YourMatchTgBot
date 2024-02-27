using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem;

public interface IStateHandler
{
    public Task RequestToUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken);

    public Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user,
        CancellationToken cancellationToken);
}