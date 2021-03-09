using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CharBot.Properties;
using CharBot.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace CharBot
{
    class Program
    {
        private DiscordSocketClient _client;

        // Documentation: https://docs.stillu.cc/guides/introduction/intro.html
        // Discord Token: https://discord.com/developers/applications/
        // Set Environment Variable: CharBot (Properties > Debug)


        // There is no need to implement IDisposable like before as we are
        // using dependency injection, which handles calling Dispose for us.
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Console.Title = "CharBot";

            // You should dispose a service provider created using ASP.NET
            // when you are finished using it, at the end of your app's lifetime.
            // If you use another dependency injection framework, you should inspect
            // its documentation for the best way to do this.
            using (var services = ConfigureServices())
            {
                _client = services.GetRequiredService<DiscordSocketClient>();
                _client.Log += LogAsync;
                _client.MessageReceived += MessageReceivedAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                // Tokens should be considered secret data and never hard-coded.
                // We can read from the environment variable to avoid hardcoding.

                await _client.LoginAsync(TokenType.Bot, Resources.DiscordToken);
                await _client.StartAsync();
                await _client.SetGameAsync("@CharBot help", "https://line98.dev");

                // Here we initialize the logic required to register our commands.
                await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

                await Task.Delay(Timeout.Infinite);
            }
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());

            return Task.CompletedTask;
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        // Do not make static, will break references to library
        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<HttpClient>()
                .AddSingleton<PictureService>()
                .BuildServiceProvider();
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message == null) return; 
            // The bot should never respond to itself.
            if (message.Author.Id == _client.CurrentUser.Id)
                return;

            if (message.Content.ToLower().Contains("adam"))
            {

                if (Emote.TryParse("<:AdamScream:813175595608965150>", out var emote))
                {
                    await message.AddReactionAsync(emote);
                }
            }
            if (message.Content.ToLower().Contains( "?"))
            {

                if (Emote.TryParse("<:Wot:813863839677415434>", out var emote))
                {
                    await message.AddReactionAsync(emote);
                }
            }
            if (message.Content.ToLower().Contains("chelsea"))
            {

                if (Emote.TryParse("<:Chelsea:674617329962975243>", out var emote))
                {
                    await message.AddReactionAsync(emote);
                }
            }
            if (message.Content.ToLower().Contains("disappointed"))
            {

                if (Emote.TryParse("<:DammitAdam:813863745734049832>", out var emote))
                {
                    await message.AddReactionAsync(emote);
                }
            }
            if (message.Content.ToLower().Contains("uwu"))
            {

                if (Emote.TryParse("<:uwu:810943834392887388>", out var emote))
                {
                    await message.AddReactionAsync(emote);
                }
            }
        }
    }
}