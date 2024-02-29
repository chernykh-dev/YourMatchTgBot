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

        builder.Services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(builder.Configuration.GetSection("DatabaseConfig")["PostgresSQL"]);
        }, contextLifetime: ServiceLifetime.Singleton);
        
        builder.Services.AddReflectionServices();
        builder.Services.AddSingleton<ITelegramBotClient>(
            new TelegramBotClient("6840671790:AAFa-HhMJZXiNL7KLqY1enC4A87rUOj_w-g"));
        builder.Services.AddSingleton<StateMachine>();
        builder.Services.AddSingleton<IInterestService, InterestService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<ICityService, CityService>();
        builder.Services.AddSingleton<UserProfileService>();
        builder.Services.AddHostedService<Worker>();

        /*
        builder.Services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = "localhost";
            opt.InstanceName = "local";
        });
        */

        var host = builder.Build();

        using var context = host.Services.GetService<ApplicationDbContext>();
        
        context.Database.Migrate();

        host.Run();
    }

    public static void ChangeCultureInfo(string languageCode)
    {
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(languageCode);
    }
}