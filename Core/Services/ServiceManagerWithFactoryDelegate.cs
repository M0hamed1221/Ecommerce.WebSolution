using ServicesAbstracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    class ServiceManagerWithFactoryDelegate(Func<IProdectService> productfactory
                                            , Func<IBasketService> baskectfactory
                                            , Func<IAuthenticationService> authenticationfactory
                                            , Func<IOrderService> orderfactory) : IServiceManager
    {
        public IProdectService prodectService => productfactory.Invoke();

        public IBasketService basketService => baskectfactory.Invoke();

        public IAuthenticationService AuthenticationService => authenticationfactory.Invoke();

        public IOrderService OrderService => orderfactory.Invoke();
    }
}
