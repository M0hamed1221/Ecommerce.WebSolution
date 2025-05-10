using Domain.Contracts;
using Domain.Models.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _dataBase=_connection.GetDatabase() ;
        public async Task DeleteAsync(string id)
        {
            await _dataBase.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetAsync(string id)
        {
          var basket = await _dataBase.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>( basket!);
            
        }
        public async Task<CustomerBasket?> CreateUpdate(CustomerBasket basket , TimeSpan? timeToLive)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _dataBase.StringSetAsync(basket.Id, jsonBasket, timeToLive ?? TimeSpan.FromDays(7));
            return IsCreatedOrUpdated ? await  GetAsync(basket.Id) : null;
        }

       
    }
}
