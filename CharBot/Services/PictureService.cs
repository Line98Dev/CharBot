using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CharBot.Models;
using Newtonsoft.Json;

namespace CharBot.Services
{
    public class PictureService
    {
        private readonly HttpClient _http;

        public PictureService(HttpClient http)
            => _http = http;

        public async Task<Stream> GetCatPictureAsync()
        {
            var response = await _http.GetAsync("https://cataas.com/cat");
            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetDogPictureAsync()
        {
            var response = await _http.GetAsync("https://dog.ceo/api/breeds/image/random");
            var dogString = await response.Content.ReadAsStringAsync();
            var dogPicture = JsonConvert.DeserializeObject<Dog>(dogString).Message;
            var resp = await _http.GetAsync(dogPicture);
            return await resp.Content.ReadAsStreamAsync();
        }
    }
}
