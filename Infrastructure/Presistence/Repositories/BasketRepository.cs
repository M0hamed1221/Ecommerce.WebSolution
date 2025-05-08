using Domain.Contracts;
using Domain.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public Task<CustomerBasket?> CreateUpdate(CustomerBasket customerBasket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket?> GetAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
