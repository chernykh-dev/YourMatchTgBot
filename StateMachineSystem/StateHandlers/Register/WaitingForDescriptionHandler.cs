using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForDescription)]
public class WaitingForDescriptionHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForDescriptionHandler(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        IReplyMarkup replyKeyboardMarkup = new ReplyKeyboardRemove();
        
        var chat = await botClient.GetChatAsync(update.Message.Chat, cancellationToken: cancellationToken);
        
        var userDescription = chat.Bio;

        if (userDescription != null)
        {
            replyKeyboardMarkup = GetReplyKeyboard(new[] { new string[] { _localizer["GetProfileDescription"] } });
        }
        
        await botClient.SendTextMessageAsync(update.Message.Chat,
            _localizer["WaitingDescription"],
            replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var userDescription = update.Message.Text;

        if (userDescription == _localizer["GetProfileDescription"])
        {
            var chat = await botClient.GetChatAsync(update.Message.Chat, cancellationToken: cancellationToken);
        
            userDescription = chat.Bio;

            if (userDescription == null)
                return;
        }
        
        user.Description = userDescription;
        user.State = BotState.Register_ShowProfile;
    }
}