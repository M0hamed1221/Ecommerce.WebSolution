using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public sealed class DeliveryMethodNotFoudException(int id):NotFoundException( $"DeliveryMethod with thid id {id} id not found ")
    {
    }
}
