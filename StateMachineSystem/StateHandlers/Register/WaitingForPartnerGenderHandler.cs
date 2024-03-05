using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using YourMatchTgBot.Models;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForPartnerGender)]
public class WaitingForPartnerGenderHandler : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;

    public WaitingForPartnerGenderHandler(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var keyboardButtons = new List<List<string>> { new() { _localizer["PartnerMan"], _localizer["PartnerAll"], _localizer["PartnerWomen"] } };

        if (user.PartnerGender != null)
        {
            keyboardButtons.Insert(0, new () { _localizer["LeaveCurrent"] + _localizer[user.PartnerGender.ToString()] });
        }
        
        var replyKeyboardMarkup = GetReplyKeyboard(keyboardButtons);

        await botClient.SendTextMessageAsync(update.Message.Chat,
            _localizer["WaitingPartnerGender"],
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var userInput = update.Message.Text;

        if (userInput.Contains(_localizer["LeaveCurrent"]))
        {
            user.State = BotState.Register_WaitingForInterests;

            return;
        }

        if (userInput == _localizer["PartnerMan"])
        {
            user.PartnerGender = Gender.Man;
        }
        else if (userInput == _localizer["PartnerAll"])
        {
            user.PartnerGender = Gender.All;
        }
        else if (userInput == _localizer["PartnerWomen"])
        {
            user.PartnerGender = Gender.Women;
        }
        else
        {
            await botClient.SendTextMessageAsync(update.Message.Chat, _localizer["Error_IncorrectVariant"],
                cancellationToken: cancellationToken);
            
            return;
        }

        user.State = BotState.Register_WaitingForInterests;
    }
}