using Domain.Contracts;
using ServicesAbstracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    class Cachservice(ICachRepository _cacheRepository) : ICachService
    {
        public async Task<string?> GetAsync(string key)
        => await _cacheRepository.GetAsync(key);

        public async Task SetAsync(string key, object value, TimeSpan expiration)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var serializedValue = JsonSerializer.Serialize(value, options);
            await _cacheRepository.SetAsync(key, serializedValue, expiration);

        }
    }
}
