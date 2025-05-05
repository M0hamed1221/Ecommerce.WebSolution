using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstracion
{
    public interface IServiceManager
    {
        public IProdectService prodectService { get; }
    }
}
