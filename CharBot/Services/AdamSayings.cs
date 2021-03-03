using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LineBot.Models;
using Newtonsoft.Json.Linq;

namespace CharBot.Services
{
    internal static class AdamSayings
    {

        private static async Task<List<AdamSaying>> GetListOfSayings()
        {
            var array = JArray.Parse(await File.ReadAllTextAsync(@"AdamSayings.json"));
            var sayings = array?.ToObject<List<AdamSaying>>() ?? new List<AdamSaying>();
            return sayings;
        }

        private static void WriteSayingsToFile(List<AdamSaying> sayings)
        {
            var array = new JArray(
                sayings.Select(s => new JObject
                {
                    {"saying", s.Saying},
                    {"timestamp", s.Timestamp}
                })
            );
            var objects = new JObject {["sayings"] = array};
            File.WriteAllTextAsync(@"AdamSayings.json", array.ToString());
        }

        public static async Task<AdamSaying> GetRandomSaying()
        {
            var sayings = await  GetListOfSayings();
            var size = sayings.Count;
            var random = new Random();
            var index = random.Next(size);
            return sayings[index];
        }

        public static async void AddSaying(string message)
        {
            var sayings = await GetListOfSayings();

            var timestamp = DateTime.Now;
            var saying = new AdamSaying(message, timestamp);
            sayings.Add(saying);
            WriteSayingsToFile(sayings);
        }
    }
}
