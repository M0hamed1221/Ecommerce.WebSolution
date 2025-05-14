using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public sealed class UserNotFoundException(string Email):NotFoundException($"User with this Email {Email} is not Found")
    {

    }
}
