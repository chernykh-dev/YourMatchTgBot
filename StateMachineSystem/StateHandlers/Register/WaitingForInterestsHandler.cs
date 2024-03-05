using System.Text;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using YourMatchTgBot.Models;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_WaitingForInterests)]
public class WaitingForInterestsHandler : StateHandlerWithKeyboardMarkup
{
    private const int MAX_INTERESTS_COUNT = 3;
    
    private readonly IInterestService _interestService;
    private IStringLocalizer<Program> _localizer;

    public WaitingForInterestsHandler(IInterestService interestService, IStringLocalizer<Program> localizer)
    {
        _interestService = interestService;
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        using var interestsEnumerator = _interestService.GetInterests().AsEnumerable().GetEnumerator();
        interestsEnumerator.MoveNext();
        
        var replyKeyboardTexts = new List<List<string>>();

        if (user.Interests.Count > 0)
        {
            var userInterests = new StringBuilder();
            foreach (var interest in user.Interests)
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

                if (user.TemporaryInterests.Any(i => i.Interest.Name == interestName))
                    localizedInterestName = "​";

                replyKeyboardTexts[i].Add(localizedInterestName);
                interestsEnumerator.MoveNext();
            }
        }

        var replyKeyboardMarkup = GetReplyKeyboard(replyKeyboardTexts);

        await botClient.SendTextMessageAsync(update.Message.Chat, 
            string.Format(_localizer["WaitingInterests"], user.TemporaryInterests.Count, MAX_INTERESTS_COUNT),
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        var messageText = update.Message.Text;

        if (user.Interests.Count > 0 && messageText.Contains(_localizer["LeaveCurrentInterests"]))
        {
            user.State = BotState.Register_WaitingForHeight;

            return;
        }
        
        user.Interests.Clear();

        var userInterest = messageText[..2];

        var interest = _interestService.GetInterestByName(userInterest);

        if (interest == null)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat, 
                _localizer["Error_IncorrectVariant"],
                cancellationToken: cancellationToken);
            
            return;
        }
        
        user.TemporaryInterests.Add(new TempInterest
        {
            User = user,
            Interest = interest
        });

        if (user.TemporaryInterests.Count == MAX_INTERESTS_COUNT)
        {
            user.Interests.AddRange(user.TemporaryInterests.Select(ti => ti.Interest));
            user.TemporaryInterests.Clear();
            
            user.State = BotState.Register_WaitingForHeight;
        }
    }
}