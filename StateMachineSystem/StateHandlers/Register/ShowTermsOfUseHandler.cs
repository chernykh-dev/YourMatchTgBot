using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_ShowTermsOfUse, BotState.Register_WaitingForTgUsername, MessageType.Text)]
public class ShowTermsOfUseHandler : StateHandlerWithKeyboardMarkup
{
    private const string TERMS_OF_USE_FILE_ID =
        "BQACAgIAAxkBAAILTWXnD80D5AxAVQouW45utC58hlYYAALNRAACL1s4SxbNc_t5P2VoNAQ";

    private const string PRIVACY_POLICY_FILE_ID =
        "BQACAgIAAxkBAAILVGXnEHUKWhSWRZYzIFBGjKeKx3raAALcRAACL1s4S8BgzIfc7iSwNAQ";
    
    private readonly IStringLocalizer<Program> _localizer;

    public ShowTermsOfUseHandler(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        await botClient.SendMediaGroupAsync(update.Message.Chat, new IAlbumInputMedia[]
        {
            new InputMediaDocument(InputFile.FromFileId(TERMS_OF_USE_FILE_ID)),
            new InputMediaDocument(InputFile.FromFileId(PRIVACY_POLICY_FILE_ID))
        }, cancellationToken: cancellationToken);
        

        var keyboardButtons = new List<List<string>> { new() { _localizer["No"], _localizer["Yes"] } };
        
        var replyKeyboardMarkup = GetReplyKeyboard(keyboardButtons);

        await botClient.SendTextMessageAsync(update.Message.Chat, _localizer["AcceptTerms"],
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public override async Task ResponseFromUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        if (update.Message.Text == _localizer["Yes"])
        {
            ChangeState(user);
        }
        else if (update.Message.Text == _localizer["No"])
        {
            await botClient.SendTextMessageAsync(update.Message.Chat, _localizer["NotAcceptTerms"],
                cancellationToken: cancellationToken);
        }
    }
}