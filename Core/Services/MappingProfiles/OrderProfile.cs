using AutoMapper;
using Domain.Models.Order;
using Domain.Models.Products;
using Microsoft.Extensions.Configuration;
using Shared.Authentication;
using Shared.DTOs.Products;
using Shared.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
  public  class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddress,AddressDto>()
                .ReverseMap();

            CreateMap<OrderItems, OrderItemsDto>()
                .ForMember(des => des.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>( ))
                .ReverseMap();

            CreateMap<Order, OrderResponse>()
                .ForMember(des => des.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                 .ForMember(des => des.Total, opt => opt.MapFrom(src => src.DeliveryMethod.Price+src.SubTotal)
                );
            CreateMap<DeliveryMethod, DeliveryMethodResponse>()
                              .ReverseMap();
        }
        public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItems, OrderItemsDto, string>
        {
            public string Resolve(OrderItems source, OrderItemsDto destination, string destMember, ResolutionContext context)
            {
                if (!string.IsNullOrWhiteSpace(source.PictureUrl))
                {

                    return $"{_configuration["BaseUrl"]}{source.PictureUrl}";

                }
                return string.Empty;
            }
        }

    }
    
}
