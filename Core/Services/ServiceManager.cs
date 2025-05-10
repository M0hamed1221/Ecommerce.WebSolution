using AutoMapper;
using Domain.Contracts;
using Services.Specfications;
using ServicesAbstracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
     public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository _basketRepository) : IServiceManager
    {
        private readonly Lazy<IProdectService> _LazyproductService=
            new Lazy<IProdectService>(()=>new ProductService(_unitOfWork, _mapper));
        public IProdectService prodectService => _LazyproductService.Value;

        private readonly Lazy<IBasketService> _LazyBasketService =
           new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IBasketService  basketService => _LazyBasketService.Value;
    }
}
