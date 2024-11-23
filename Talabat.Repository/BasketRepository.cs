using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repostories.Contract;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepositoty
    {

        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketid)
        {
            return await _database.KeyDeleteAsync(basketid);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketid)
        {
            var basket = await _database.StringGetAsync(basketid);
            var convertJson = JsonSerializer.Deserialize<CustomerBasket>(basket);
            return basket.IsNullOrEmpty ? null : convertJson;
        }


        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var SeralizaBasket = JsonSerializer.Serialize(basket);
            var CreatedOrUpdated = await _database.StringSetAsync(basket.Id, SeralizaBasket, TimeSpan.FromDays(30));

            if (!CreatedOrUpdated)
            {
                return null;
            }

            return await GetBasketAsync(basket.Id);

        }

    }
}
