using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModles
{
   public class VaildationErrorModel
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
        public string ErrorMessage { get; set; } = "Validation Os MOdel State Failed";

        public IEnumerable<VaildationError> vaildationErrors { get; set; } = [];
    }
}
