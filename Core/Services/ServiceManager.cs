using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Specfications;
using ServicesAbstracion;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
     public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository _basketRepository,UserManager<ApplicationUser>  _userManager,IOptions<JWTOptions> _jwtoptions) : IServiceManager
    {
        private readonly Lazy<IProdectService> _LazyproductService=
            new Lazy<IProdectService>(()=>new ProductService(_unitOfWork, _mapper));
        public IProdectService prodectService => _LazyproductService.Value;
        private readonly Lazy<IOrderService> _lazyOrderService =
            new Lazy<IOrderService>(() => new OrderService(_basketRepository, _unitOfWork, _mapper));
        public IOrderService OrderService => _lazyOrderService.Value;
        private readonly Lazy<IBasketService> _LazyBasketService =
           new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IBasketService  basketService => _LazyBasketService.Value;

        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService =
           new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _jwtoptions,_mapper));
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;
    }
}
