using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Discord;
using Discord.Commands;
using LineBot.Services;

namespace LineBot.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        public PublicModule(PictureService pictureService)
        {
            PictureService = pictureService;
        }

        // Dependency Injection will fill this value in for us
        private PictureService PictureService { get; }

        [Command("creator")]
        [Alias("website")]
        public Task CreatorAsync()
            => ReplyAsync("I was created by https://line98.dev");

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
            => ReplyAsync("pong!");

        [Command("cat")]
        public async Task CatAsync()
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatPictureAsync();
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }

        [Command("adam")]
        public async Task AdamSayingAsync()
        {
            var saying = await AdamSayings.GetRandomSaying();
            var color = Color.Blue;
            var timestamp = saying.Timestamp;
            var author = new EmbedAuthorBuilder()
                .WithName("Adam's Sayings")
                .WithIconUrl("https://cdn.discordapp.com/attachments/813082051947134998/813174831150530560/AdamSayings.jpg");
            var embed = new EmbedBuilder
            {
                Title = saying.Saying,
                Author = author,
                Color = color,
                Timestamp = timestamp
            };

            await ReplyAsync(embed: embed.Build());
        }

        [Command("AddSaying")]
        public async Task AddAdamSayingAsync([Remainder]string saying)
        {
            AdamSayings.AddSaying(saying);
            await ReplyAsync("Added \"" + saying + "\"");

        }

        [Command("embed")]
        public async Task EmbedMessageAsync()
        {
            
            var color = new Color(19, 144, 255);
            var timestamp = DateTime.Now;
            var author = new EmbedAuthorBuilder()
                .WithName("LineBot")
                .WithIconUrl("https://cdn.discordapp.com/attachments/813082051947134998/813172660707000370/Pixel_Hunter_Circle.png");
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

        // Ban a user
        [Command("ban")]
        [RequireContext(ContextType.Guild)]
        // make sure the user invoking the command can ban
        [RequireUserPermission(GuildPermission.BanMembers)]
        // make sure the bot itself can ban
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
        {
            await user.Guild.AddBanAsync(user, reason: reason);
            await ReplyAsync("ok!");
        }

        // [Remainder] takes the rest of the command's arguments as one argument, rather than splitting every space
        [Command("echo")]
        public Task EchoAsync([Remainder] string text)
            // Insert a ZWSP before the text to prevent triggering other bots!
            => ReplyAsync('\u200B' + text);

        // 'params' will parse space-separated elements into a list
        [Command("list")]
        public Task ListAsync(params string[] objects)
            => ReplyAsync("You listed: " + string.Join("; ", objects));

        [Command("commands")]
        [RequireContext(ContextType.Guild)]
        public Task ListServerCommand()
            => ReplyAsync("View the list of commands at https://bot.line98.dev/" + Context.Guild.Id + "/commands/");

        [Command("commands")]
        [RequireContext(ContextType.DM)]
        public Task ListBasicCommand()
            => ReplyAsync("View the list of commands at https://bot.line98.dev/commands/");
    }
}
