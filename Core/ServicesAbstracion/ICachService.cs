using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstracion
{
    public interface ICachService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, object value, TimeSpan expiration);
    }
}
