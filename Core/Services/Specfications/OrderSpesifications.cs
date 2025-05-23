using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specfications
{
    public class OrderSpesifications : BaseSpecifications<Order>
    {
        //for get by id
        public OrderSpesifications(Guid id):base(o=>o.Id==id)
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);
        }
        // for get all
        public OrderSpesifications(string email) : base(x =>  x.UserName == email)
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);

        }
    }
}
    
