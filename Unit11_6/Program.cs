using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using Unit11_6;
using Unit11_6.Configuration;
using Unit11_6.Controllers;
using Unit11_6.Services;

namespace Unit11_6;
public class Program
{
    public static async Task Main()
    {
        Console.OutputEncoding = Encoding.Unicode;

        var host = new HostBuilder()
            .ConfigureServices((hostContext, services) => ConfigureServices(services))
            .UseConsoleLifetime()
            .Build();

        Console.WriteLine("Сервис запущен");
        await host.RunAsync();
        Console.WriteLine("Сервис остановлен");
    }
    static AppSettings BuildAppSettings()
    {
        return new AppSettings
        { 
            BotToken = "5576984963:AAFXnJtWHFzGL6rBGesuwD3JXogWrJ6i_JM",
        };
    }

    static void ConfigureServices(IServiceCollection services)
    {
        AppSettings appSettings = BuildAppSettings();
        services.AddSingleton<IStorage, MemoryStorage>();
        services.AddTransient<DefaultMessageController>();
        services.AddTransient<TextMessageController>();
        services.AddTransient<InlineKeyboardController>();

        services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
        services.AddHostedService<Bot>();
    }
}