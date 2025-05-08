using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public sealed class ProductNotFoudException(int id)
        :NotFoundException($"Product With this ID {id} is Not Found")
    {
    }
}
