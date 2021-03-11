using CarPriceAPI.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarPriceAPI.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly HttpClient _client;

        public HistoryService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new("application/json"));
        }

        public async Task AddCarHistoryDbAsync(CarHistoryModel carHistoryModel)
        {
            //var response = _client.PostAsJsonAsync("api/history", carHistoryModel);


            var response = await _client.PostAsync("api/history", JsonContent.Create(carHistoryModel));

            var a = 2;
        }
    }
}
