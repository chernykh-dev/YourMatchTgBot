using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourMatchTgBot.Models;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForPartnerGender, BotState.WatchProfiles, MessageType.Text)]
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
            keyboardButtons.Insert(0, new () { _localizer["LeaveCurrent"] + _localizer["Partner" + user.PartnerGender] });
        }
        
        var replyKeyboardMarkup = GetReplyKeyboard(keyboardButtons);

        await botClient.SendTextMessageAsync(user.Id,
            _localizer["WaitingPartnerGender"],
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var userInput = update.Message?.Text;

        if (userInput.Contains(_localizer["LeaveCurrent"]))
        {
            ChangeState(user);

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
            await botClient.SendTextMessageAsync(user.Id, _localizer["Error_IncorrectVariant"],
                cancellationToken: cancellationToken);
            
            return;
        }

        ChangeState(user);
    }
}