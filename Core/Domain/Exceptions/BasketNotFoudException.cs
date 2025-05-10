using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketNotFoudException(string id)
        : NotFoundException($"Basket With this ID {id} is Not Found")
    {
    }
}
