using Shared.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstracion
{
   public interface IBasketService
    {
        Task<BasketDto> GetAsync(string id);

        Task<BasketDto> UpdateAsync(BasketDto BasketDtoid);
        Task DeleteAsync(string id);


    }
}
