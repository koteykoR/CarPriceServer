using CarPriceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarPriceAPI.Services
{
    public class ParserService : IParserService
    {
        private readonly HttpClient _client;

        public ParserService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CarModel>> GetCars(CarModel carModel)
        {
            IEnumerable<CarModel> cars = null;

            var response = await _client.PostAsJsonAsync("api/parser", carModel);

            if(response.IsSuccessStatusCode)
            {
                cars = await response.Content.ReadFromJsonAsync<IEnumerable<CarModel>>();
            }

            return cars;
        }
    }
}
