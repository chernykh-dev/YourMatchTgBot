using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForDescription, BotState.Register_ShowProfile, MessageType.Text)]
public class WaitingForDescriptionHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForDescriptionHandler(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var keyboardButtons = new List<List<string>>();

        if (user.Description != null)
        {
            keyboardButtons.Add(new () { _localizer["LeaveCurrentDescription"] });
        }
        
        var chat = await botClient.GetChatAsync(user.Id, cancellationToken: cancellationToken);
        
        var userDescription = chat.Bio;

        if (userDescription != null)
        {
            keyboardButtons.Add(new () { _localizer["GetProfileDescription"] });
        }

        var replyKeyboard = GetReplyKeyboard(keyboardButtons);
        
        await botClient.SendTextMessageAsync(user.Id,
            _localizer["WaitingDescription"],
            replyMarkup: replyKeyboard, cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var userDescription = update.Message.Text;

        if (userDescription == _localizer["LeaveCurrentDescription"])
        {
            ChangeState(user);
            
            return;
        }
        
        if (userDescription == _localizer["GetProfileDescription"])
        {
            var chat = await botClient.GetChatAsync(user.Id, cancellationToken: cancellationToken);
        
            userDescription = chat.Bio;

            if (userDescription == null)
                return;
        }

        if (userDescription.Length > 120)
        {
            await botClient.SendTextMessageAsync(user.Id,
                _localizer["Error_LongDescription"],
                cancellationToken: cancellationToken);

            return;
        }
        
        user.Description = userDescription;
        ChangeState(user);
    }
}