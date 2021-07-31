using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CharBot.Services;
using Discord;
using Discord.Commands;

namespace CharBot.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        // Dependency Injection will fill this value in for us
        // ReSharper disable once MemberCanBePrivate.Global
        public PictureService PictureService { get; set; }
        private readonly Color _cardinalRed = new(186, 12, 47);

        [Command("creator")]
        [Alias("website")]
        [Summary("See who made me! (hint: it's Hunter).")]
        public Task CreatorAsync([Remainder] string text = null)
            => ReplyAsync("I was created by https://line98.dev");

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync([Remainder] string text = null)
            => ReplyAsync("pong!");

        [Command("cat")]
        public async Task CatAsync([Remainder] string text = null)
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatPictureAsync();
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }

        [Command("dog")]
        public async Task DogAsync([Remainder] string text = null)
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetDogPictureAsync();
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "dog.png");
        }

        [Command("uptime")]
        public async Task UptimeAsync([Remainder] string text = null)
        {
            TimeSpan time = DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime();
            await ReplyAsync("I've been awake for " + time.ToString(@"dd\:hh\:mm\:ss") + "!");
        }

        [Command("embed")]
        public async Task EmbedMessageAsync([Remainder] string text = null)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));

            var color = new Color(19, 144, 255);
            var timestamp = DateTime.Now;
            var author = new EmbedAuthorBuilder()
                .WithName("LineBot")
                .WithIconUrl(
                    "https://cdn.discordapp.com/attachments/813082051947134998/813172660707000370/Pixel_Hunter_Circle.png");
            var embed = new EmbedBuilder
            {
                Title = "Test Title",
                Description = "Test Description.  This is a blurb of text",
                Author = author,
                Color = color,
                Timestamp = timestamp
            };
            await ReplyAsync(embed: embed.Build());
        }

        // Get info on a user, or the user who invoked the command if one is not specified
        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user ??= Context.User;

            await ReplyAsync(user.ToString());
        }

        // [Remainder] takes the rest of the command's arguments as one argument, rather than splitting every space
        [Command("echo")]
        public Task EchoAsync([Remainder] string text = null)
            // Insert a ZWSP before the text to prevent triggering other bots!
            => ReplyAsync('\u200B' + text);

        [Command("help")]
        [Alias("commands")]
        [RequireContext(ContextType.Guild)]
        public async Task ListCommands([Remainder] string text = null)
        {
            var author = new EmbedAuthorBuilder()
                .WithName("CharBot")
                .WithIconUrl(
                    "https://cdn.discordapp.com/emojis/768902970036584508.png?v=1");
            var embed = new EmbedBuilder
            {
                Title = "Commands",
                Description = "The list of commands are below. Prepend a command with ! or @ me to call them. All commands are case-insensitive.\nWant something added? Let Hunter know!" ,
                Author = author,
                Color = _cardinalRed
            };
            embed.AddField("links", "Get the list of work links.");
            embed.AddField("char", "See what I look like.");
            embed.AddField("charHeadShot", "Get that close up on me.");
            embed.AddField("tyler", "See what everyone's favorite streamer looks like.");
            embed.AddField("addLink", "Add a link to the list (Format: addlink \"name\"  \"url\").");
            embed.AddField("creator | website", "See who made me! (hint: it's Hunter).");
            embed.AddField("ping | pong | hello", "Make sure I'm alive.");
            embed.AddField("cat", "Get a random cat picture from thecatapi.com.");
            embed.AddField("dog", "Get a random cat picture from dog.ceo/dog-api/.");
            embed.AddField("echo", "I'll repeat whatever you say.");
            embed.AddField("help", "Display this block again.");
            embed.AddField("uptime", "See how long CharBot has been going.");

            await ReplyAsync(embed: embed.Build());
        }
    }
}
