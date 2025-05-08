using AutoMapper;
using Domain.Models.Products;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
   public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            // source => Destenation
            CreateMap<Product, ProductResponse>()
                    .ForMember(des=>des.BrandName,
                               options=>options.MapFrom(scr=>scr.ProductBrand.Name));
            CreateMap<Product, ProductResponse>()
         .ForMember(des => des.TypeName,
                    options => options.MapFrom(scr => scr.ProductType.Name))
         .ForMember(dec=>dec.PictureUrl,option=>option.MapFrom<PictureUrlResolver>());
       

            CreateMap<ProductBrand, BrandResponse>();
            CreateMap<ProductType, TypeResponse>();




        }
        public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResponse, string>
        {
            public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
            {
               if(!string.IsNullOrWhiteSpace(source.PictureUrl))
                    {

                    return $"{_configuration["BaseUrl"]}{source.PictureUrl}";

                }
                return string.Empty;
            }
        }
    }
}
