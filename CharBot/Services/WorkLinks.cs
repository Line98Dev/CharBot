using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CharBot.Models;
using Newtonsoft.Json.Linq;

namespace CharBot.Services
{
    internal static class WorkLinks
    {

        private static async Task<List<Link>> GetLinks()
        {
            var array = JArray.Parse(await File.ReadAllTextAsync(@"links.json"));
            var links = array?.ToObject<List<Link>>() ?? new List<Link>();
            return links.OrderBy(x => x.Name).ToList();
        }

        private static void WriteLinksToFile(IEnumerable<Link> links)
        {
            var array = new JArray(
                links.Select(l => new JObject
                {
                    {"name", l.Name},
                    {"url", l.Url}
                })
            );
            File.WriteAllTextAsync(@"links.json", array.ToString());
        }

        public static async Task<string> GetLinkListString()
        {
            var links = await  GetLinks();

            return links.Aggregate("", (current, link) => current + (link.ToString() + "\n"));
        }

        public static async void AddLink(string name, string url)
        {
            var links = await GetLinks();

            var timestamp = DateTime.Now;
            var link = new Link(name, url);
            links.Add(link);
            WriteLinksToFile(links);
        }
    }
}
