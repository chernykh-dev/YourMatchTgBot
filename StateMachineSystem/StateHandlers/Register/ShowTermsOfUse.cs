using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem.StateHandlers.Register;

[StateHandler(BotState.Register_ShowTermsOfUse)]
public class ShowTermsOfUse : StateHandlerWithKeyboardMarkup
{
    private readonly IStringLocalizer<Program> _localizer;

    public ShowTermsOfUse(IStringLocalizer<Program> localizer)
    {
        _localizer = localizer;
    }

    public override async Task RequestToUser(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        /*
        var resources =
            $"{Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../"))}Resources/terms-of-use.ru.txt";

        var termsText = await File.ReadAllTextAsync(resources, cancellationToken);
        */
        const string termsOfUseFileId = "BQACAgIAAxkBAAIJd2Xl_eQwPheem9fUMzOV9au7G6-BAALSTAACBYwxSxLP3wsUz4gcNAQ";

        await botClient.SendDocumentAsync(update.Message.Chat, InputFile.FromFileId(termsOfUseFileId),
            cancellationToken: cancellationToken);

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
            user.State = BotState.Register_WaitingForName;
        }
        else if (update.Message.Text == _localizer["No"])
        {
            await botClient.SendTextMessageAsync(update.Message.Chat, _localizer["NotAcceptTerms"],
                cancellationToken: cancellationToken);
        }
    }
}