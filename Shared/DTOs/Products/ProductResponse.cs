using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Products
{
   public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Discription { get; set; }
        public string PictureUrl { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
        public Decimal Price { get; set; }





    }
}
