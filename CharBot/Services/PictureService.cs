using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
            _http.DefaultRequestHeaders.Add("x-api-key", "2b082ff5-0c88-4ed6-b83a-7321dc186e88");
            var response = await _http.GetAsync("https://api.thecatapi.com/v1/images/search?limit=1");
            var catString = await response.Content.ReadAsStringAsync();
            var catList = JsonConvert.DeserializeObject<List<Cat>>(catString);
            var resp = await _http.GetAsync(catList[0].Url);
            return await resp.Content.ReadAsStreamAsync();
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
