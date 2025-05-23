using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public enum PaymentStatus
    {
        Pending =0,
        Failed =1,
        Succeeded=2 
    }
    public class Order: BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(List<OrderItems> items, OrderAddress address , decimal subtotal,string email , DeliveryMethod method)
        {
            OrderItems = items;
            OrderAddress = address;
            SubTotal = subtotal;
            UserName = email;
            DeliveryMethod = method;
        }

        public string UserName { get; set; }
       
        public List<OrderItems> OrderItems { get; set; } =[];
        public OrderAddress OrderAddress { get; set; }

        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }

        public decimal SubTotal { get; set; }
        public DateTimeOffset date { get; set; } = DateTimeOffset.Now;

        public string PaymentIntentId { get; set; }=string.Empty;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;




    }

}

