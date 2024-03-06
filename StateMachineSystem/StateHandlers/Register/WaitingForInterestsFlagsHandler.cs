using System.Collections.Specialized;
using System.Text;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourMatchTgBot.Models;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForInterests, BotState.Register_WaitingForHeight, MessageType.Text)]
public class WaitingForInterestsFlagsHandler : StateHandlerWithKeyboardMarkup
{
    private const int MAX_INTERESTS_COUNT = 3;
    
    private readonly IInterestService _interestService;
    private IStringLocalizer<Program> _localizer;

    public WaitingForInterestsFlagsHandler(IInterestService interestService, IStringLocalizer<Program> localizer)
    {
        _interestService = interestService;
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        using var interestsEnumerator = _interestService.GetInterests().AsEnumerable().GetEnumerator();
        interestsEnumerator.MoveNext();
        
        var replyKeyboardTexts = new List<List<string>>();

        if (user.InterestsFlags > 0)
        {
            var interests = _interestService.GetInterestsByIds(GetInterestsFlags(user.InterestsFlags).ToArray());
            
            var userInterests = new StringBuilder();
            foreach (var interest in interests)
            {
                userInterests.Append(interest.Name);
            }
            
            replyKeyboardTexts.Add(new () { _localizer["LeaveCurrentInterests"] + userInterests });
        }

        var index = replyKeyboardTexts.Count;
        for (var i = index; i < 5 + index; i++)
        {
            replyKeyboardTexts.Add(new List<string>());
            for (var j = 0; j < 3 - i % 2; j++)
            {
                var interestName = interestsEnumerator.Current.Name;

                var localizedInterestName = $"{interestName} {_localizer[interestName]}";

                if (GetInterestsFlags(user.TemporaryInterestsFlags).Any(i => i == interestsEnumerator.Current.Id))
                    localizedInterestName = "​";

                replyKeyboardTexts[i].Add(localizedInterestName);
                interestsEnumerator.MoveNext();
            }
        }

        var replyKeyboardMarkup = GetReplyKeyboard(replyKeyboardTexts);

        await botClient.SendTextMessageAsync(user.Id, 
            string.Format(_localizer["WaitingInterests"], GetInterestsFlags(user.TemporaryInterestsFlags).Count, MAX_INTERESTS_COUNT),
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var messageText = update.Message.Text;

        if (user.InterestsFlags > 0 && messageText.Contains(_localizer["LeaveCurrentInterests"]))
        {
            ChangeState(user);

            return;
        }

        user.InterestsFlags = 0;

        var userInterest = messageText[..2];

        var interest = _interestService.GetInterestByName(userInterest);

        if (interest == null)
        {
            await botClient.SendTextMessageAsync(user.Id, 
                _localizer["Error_IncorrectVariant"],
                cancellationToken: cancellationToken);
            
            return;
        }
        
        user.TemporaryInterestsFlags |= interest.Id;

        if (GetInterestsFlags(user.TemporaryInterestsFlags).Count == MAX_INTERESTS_COUNT)
        {
            user.InterestsFlags = user.TemporaryInterestsFlags;
            user.TemporaryInterestsFlags = 0;
            
            ChangeState(user);
        }
    }

    private List<long> GetInterestsFlags(int interestsFlags)
    {
        var ids = new List<long>();

        for (var k = 0; k < 32; k++)
        {
            var mask = 1 << k;
            var maskedId = interestsFlags & mask;
            
            if (maskedId > 0)
                ids.Add(maskedId);
        }

        return ids;
    }
}