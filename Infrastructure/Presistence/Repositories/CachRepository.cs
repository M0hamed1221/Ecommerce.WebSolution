using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Presistence.Repositories
{
   public  class CachRepository(IConnectionMultiplexer _connection) : ICachRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();

        public async Task<string?> GetAsync(string cacheKey)
        {
            var value = await _database.StringGetAsync(cacheKey);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string cacheKey, string value, TimeSpan expirations)
        {
            await _database.StringSetAsync(cacheKey, value, expirations);
        }
    }
}
