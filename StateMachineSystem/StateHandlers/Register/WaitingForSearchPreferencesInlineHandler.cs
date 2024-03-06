using Telegram.Bot;
using Telegram.Bot.Types;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForSearchPreferences, BotState.WatchProfiles)]
public class WaitingForSearchPreferencesInlineHandler : AbstractStateHandler
{
    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        if (update.CallbackQuery is not { } callbackQuery)
            return;
    }
}