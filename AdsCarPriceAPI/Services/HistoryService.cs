using AdsCarPriceAPI.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AdsCarPriceAPI.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly HttpClient _client;

        public HistoryService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task AddCarHistoryDbAsync(CarHistoryModel carHistoryModel)
        {
            await _client.PostAsJsonAsync("api/history", carHistoryModel);
        }
    }
}
