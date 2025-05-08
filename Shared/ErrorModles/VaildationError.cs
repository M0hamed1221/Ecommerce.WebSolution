using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModles
{
   public class VaildationError
    {
       public string Filed { get; set; }
        public IEnumerable<string> Errors { get; set; } = [];

    }
}
