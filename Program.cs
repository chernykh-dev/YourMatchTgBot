using YourMatchTgBot.Services;

namespace YourMatchTgBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddSingleton<InterestService>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}