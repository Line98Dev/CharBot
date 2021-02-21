using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LineBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LineBot.Services
{
    class AdamSayings
    {
        private List<AdamSaying> listOfSayings;

        public AdamSayings()
        {
            GetListOfSayings();
        }

        private static void GetListOfSayings()
        {
            var objects = JObject.Parse(File.ReadAllText(@"AdamSayings.json"));
            Console.Write(objects);
        }
    }
}
