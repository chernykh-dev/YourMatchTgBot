using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourMatchTgBot.ReflectionExtensions;
using YourMatchTgBot.Services;
using User = YourMatchTgBot.Models.User;

namespace YourMatchTgBot.StateMachineSystem;

public class StateMachine
{
    public const BotState InitialState = BotState.Start;

    private readonly ILogger<StateMachine> _logger;
    private readonly IUserService _userService;
    
    private readonly Dictionary<BotState, AbstractStateHandler> _stateHandlers = new();

    public StateMachine(IDependencyReflectorFactory dependencyReflectorFactory, IUserService userService, ILogger<StateMachine> logger)
    {
        _userService = userService;
        _logger = logger;
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            var handlerAttribute = type.GetCustomAttribute<StateHandlerAttribute>();
            if (handlerAttribute == null)
                continue;
            
            RegisterHandler(handlerAttribute.State,
                dependencyReflectorFactory.GetReflectedType<AbstractStateHandler>(type, null));
        }
    }

    private void RegisterHandler(BotState state, AbstractStateHandler handler)
    {
        _stateHandlers[state] = handler;
    }

    public async Task ActivateState(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        if (user.State == BotState.Register_WaitingForSearchPreferences && 
            update.Type == UpdateType.CallbackQuery)
        {
            
        }
        else if (update.Type == UpdateType.Message)
        {
            if (update.Message.Text == "/clear")
            {
                user.State = BotState.Start;

                await botClient.SendTextMessageAsync(update.Message.Chat, "Enter /start",
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
            
                return;
            }

            var languageCode = user.LanguageCode;

            if (languageCode == null)
            {
                languageCode = update.Message.From.LanguageCode;
            
                if (user.Id == 472106852L)
                    languageCode = "ru";
            
                user.LanguageCode = languageCode;
            }
        }
        else
        {
            return;
        }
        
        Program.ChangeCultureInfo(user.LanguageCode);

        try
        {
            if (_stateHandlers.TryGetValue(user.State, out var stateHandler) && stateHandler.AllowedMessageTypes.Contains(update.Message.Type))
            {
                await stateHandler.ResponseFromUser(botClient, update, user, cancellationToken);

                // State must be updated in response handler. ^^^
            }

            // Не понятно, почему не работает в хэндлере.
            Program.ChangeCultureInfo(user.LanguageCode);
            
            if (_stateHandlers.TryGetValue(user.State, out stateHandler))
            {
                await stateHandler.RequestToUser(botClient, update, user, cancellationToken);
            }
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, exception.Message);

            await botClient.SendTextMessageAsync(472106852L, exception.Message, cancellationToken: cancellationToken);
            
            await botClient.SendTextMessageAsync(user.Id, "Something wrong: repeat your input", cancellationToken: cancellationToken);
        }
        finally
        {
            
        }
    }
}

public enum BotState
{
    Start = 0,
    
    Register_WaitingForName,
    Register_WaitingForAge,
    Register_WaitingForGender,
    Register_WaitingForInterests,
    Register_WaitingForHeight,
    Register_WaitingForLocation,
    Register_WaitingForPhotos,
    
    Menu,
    Register_WaitingForDescription,
    Register_ShowProfile,
    WatchProfiles,
    Register_WaitingForPartnerGender,
    Register_ShowTermsOfUse,
    Register_WaitingForSearchPreferences,
    Register_WaitingForLanguage
}