using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Baskets;
using ServicesAbstracion;
using Shared.DTOs.Basket;

namespace Services.Specfications
{
    public class BasketService(IBasketRepository _basketRepository
                              , IMapper _mapper) : IBasketService
    {
       

       
        public async Task DeleteAsync(string id)
        {
            await _basketRepository.DeleteAsync(id);

        }

        public async Task<BasketDto> GetAsync(string id)
        {
            var basket = await _basketRepository.GetAsync(id) ?? throw new BasketNotFoudException(id);
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto> UpdateAsync(BasketDto basketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);
            var updatedBasket = await _basketRepository.CreateUpdate(basket)??
                throw new Exception("Can Not Create Basket");
            return _mapper.Map<BasketDto>(updatedBasket);
        }
    }
}
