using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Orders
{
  public  class OrderResponse
    {

        public Guid Id { get; set; }
        public string UserName { get; set; }

        public List<OrderItemsDto> OrderItems { get; set; } = [];
        public AddressDto OrderAddress { get; set; }

       
        public string DeliveryMethod { get; set; }

        public decimal Total { get; set; }
        public DateTimeOffset date { get; set; } = DateTimeOffset.Now;

        
        public string PaymentStatus { get; set; } 
    }
    public class OrderItemsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
