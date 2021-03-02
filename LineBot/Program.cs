using System;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LineBot.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LineBot
{
    internal static class Program
    {

        // Documentation: https://docs.stillu.cc/guides/introduction/intro.html
        // Discord Token: https://discord.com/developers/applications/
        // Set Environment Variable: LineBot (Properties > Debug)


        // There is no need to implement IDisposable like before as we are
        // using dependency injection, which handles calling Dispose for us.
        private static void Main(string[] args)
            => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            Console.Title = "LineBot";
            // You should dispose a service provider created using ASP.NET
            // when you are finished using it, at the end of your app's lifetime.
            // If you use another dependency injection framework, you should inspect
            // its documentation for the best way to do this.
            await using var services = ConfigureServices();
            var client = services.GetRequiredService<DiscordSocketClient>();

            // Tokens should be considered secret data and never hard-coded.
            // We can read from the environment variable to avoid hardcoding.

            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken"));
            await client.StartAsync();
            await client.SetGameAsync("bot.line98.dev", "bot.line98.dev", ActivityType.Watching);

            // Here we initialize the logic required to register our commands.
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

            await Task.Delay(Timeout.Infinite);
         }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<HttpClient>()
                .AddSingleton<PictureService>()
                .BuildServiceProvider();
        }
    }
}

