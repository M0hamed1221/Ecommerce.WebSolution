using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Baskets
{
   public class CustomerBasket
    {
        public int Id { get; set; }//GUID :From Client Side

        public IEnumerable<BasketItem> Id { get; set; }

    }
}
