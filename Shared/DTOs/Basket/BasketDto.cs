using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Basket
{
   public class BasketDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string PictureUrl { get; set; }
        [Range(1, 999999)]

        public decimal Price { get; set; }
        [Range(1,byte.MaxValue)]
        public int Qty { get; set; }

    }
}
