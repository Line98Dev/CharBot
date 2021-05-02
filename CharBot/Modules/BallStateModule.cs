using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharBot.Models;
using CharBot.Services;
using Discord;
using Discord.Commands;

namespace CharBot.Modules
{
    public class BallStateModule : ModuleBase<SocketCommandContext>
    {
        private readonly Color _cardinalRed = new(186, 12, 47);


        [Command("graduate")]
        [Alias("gtfo")]
        [Summary("See who made me! (hint: it's Hunter).")]
        public async Task GraduationAsync([Remainder] string text = null)
        {
            var author = new EmbedAuthorBuilder()
                .WithName("CharBot")
                .WithIconUrl(
                    "https://cdn.discordapp.com/emojis/768902970036584508.png?v=1");
            var embed = new EmbedBuilder
            {
                Title = "Graduation 2021",
                Author = author,
                Color = _cardinalRed
            };
            var timespan = Countdown.CommencementCountDown();
            foreach (var school in timespan)
            {
                embed.AddField(school.Key, school.Value);
            } 
            embed.WithCurrentTimestamp();

            await ReplyAsync(embed: embed.Build());
        }

        [Command("tyler")]
        public async Task TylerShotAsync([Remainder] string text = null)
        {
            var stream = File.OpenRead(@"Tyler.png");
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "Tyler.png");
        }

        [Command("links")]
        public async Task ListLinks([Remainder] string text = null)
        {
            var embed = new EmbedBuilder
            {
                Title = "Work Links",
                Description = await WorkLinks.GetLinkListString(),
                Color = _cardinalRed
            };

            await ReplyAsync(embed: embed.Build());
        }

        [Command("AddLink")]
        public async Task AddLinkAsync(string name, string url, [Remainder] string text = null)
        {
            WorkLinks.AddLink(name, url);
            await ReplyAsync("Added " + name + " to go to " + url + ".");
        }
    }
}
