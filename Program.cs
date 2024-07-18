using System.Globalization;
using Microsoft.EntityFrameworkCore;
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

        builder.Services.AddLocalization(opt =>
        {
            opt.ResourcesPath = "Resources";
        });

        builder.Services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Singleton);

        var botClient = new TelegramBotClient("6840671790:AAFa-HhMJZXiNL7KLqY1enC4A87rUOj_w-g");
        botClient.Timeout = TimeSpan.FromMinutes(5);

        builder.Services.AddReflectionServices();
        builder.Services.AddSingleton<ITelegramBotClient>(botClient);
        builder.Services.AddSingleton<StateMachine>();
        builder.Services.AddSingleton<IInterestService, InterestService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<ICityService, CityService>();
        builder.Services.AddSingleton<UserProfileService>();
        builder.Services.AddSingleton<UserMatchingService>();
        builder.Services.AddSingleton<MatchesService>();
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

    public static void ChangeCultureInfo(string languageCode)
    {
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(languageCode);
    }
}