using System.IO;
using System.Threading.Tasks;
using CharBot.Services;
using Discord;
using Discord.Commands;

namespace CharBot.Modules
{
    public class BallStateModule : ModuleBase<SocketCommandContext>
    {
        private readonly Color _cardinalRed = new(186, 12, 47);

        [Command("char")]
        public async Task CharAsync([Remainder] string text = null)
        {
            var stream = File.OpenRead(@"fullChar.png");
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "fullChar.png");
        }

        [Command("charHeadShot")]
        public async Task CharHeadShotAsync([Remainder] string text = null)
        {
            var stream = File.OpenRead(@"char.png");
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "char.png");
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
