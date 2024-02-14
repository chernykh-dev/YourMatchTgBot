using YourMatchTgBot.ReflectionExtensions;
using YourMatchTgBot.Services;
using YourMatchTgBot.StateMachine;

namespace YourMatchTgBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddReflectionServices();
        builder.Services
            .AddSingleton<StateMachine.StateMachine>(
                provider => new StateMachine.StateMachine(
                    State.Start, provider.GetService<IDependencyReflectorFactory>()));
        builder.Services.AddSingleton<InterestService>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}