using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICachRepository
    {
        Task<string?> GetAsync(string cacheKey);
        Task SetAsync(string cacheKey, string value, TimeSpan expirations);
    }
}
