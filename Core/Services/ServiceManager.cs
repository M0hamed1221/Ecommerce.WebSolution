using AutoMapper;
using Domain.Contracts;
using ServicesAbstracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
     public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper) : IServiceManager
    {
        private readonly Lazy<IProdectService> _LazyproductService=
            new Lazy<IProdectService>(()=>new ProductService(_unitOfWork, _mapper));
        public IProdectService prodectService => _LazyproductService.Value;
    }
}
