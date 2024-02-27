using Telegram.Bot.Types;

namespace YourMatchTgBot;

public static class UpdateExtensions
{
    public static long GetUserId(this Update update)
    {
        return update.Message.From.Id;
    }
}