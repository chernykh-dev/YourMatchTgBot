using Telegram.Bot;
using Telegram.Bot.Types;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers;

[StateHandler(BotState.WatchProfiles)]
public class WatchProfilesHandler : StateHandlerWithKeyboardMarkup
{
    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}