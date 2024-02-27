using Microsoft.Extensions.DependencyInjection.Extensions;
using Telegram.Bot;
using YourMatchTgBot.ReflectionExtensions;
using YourMatchTgBot.Services;
using YourMatchTgBot.StateMachineSystem;

namespace YourMatchTgBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddReflectionServices();
        builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient("6840671790:AAFa-HhMJZXiNL7KLqY1enC4A87rUOj_w-g"));
        builder.Services
            .AddSingleton<StateMachine>(
                provider => new StateMachine(
                    BotState.Start, provider.GetService<IDependencyReflectorFactory>()));
        builder.Services.AddSingleton<InterestService>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddHostedService<Worker>();

        /*
        builder.Services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = "localhost";
            opt.InstanceName = "local";
        });
        */

        var host = builder.Build();
        host.Run();
    }
}