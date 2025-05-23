﻿using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Baskets;
using Domain.Models.Identity;
using Domain.Models.Order;
using Domain.Models.Products;
using Services.Specfications;
using ServicesAbstracion;
using Shared.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IBasketRepository _basketRepository,IUnitOfWork _unitOfWork,IMapper _mapper) : IOrderService
    {
        public async Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest, string Email)
        { 
           var basket = await _basketRepository.GetAsync(orderRequest.BasketId);
            if (basket == null)
                throw new BasketNotFoudException(orderRequest.BasketId);

          List<OrderItems> orderItems =[];
            foreach (var item in basket.basketItems)
            {

                var OrignalItem = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)??throw new ProductNotFoudException(item.Id);


                orderItems.Add(createOrderItem(item, OrignalItem)); 

            }
            var method =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderRequest.DeliveryMethodId)??throw new DeliveryMethodNotFoudException(orderRequest.DeliveryMethodId);
                
            var address =_mapper.Map<OrderAddress> (orderRequest.Address);

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            var order = new Order(orderItems,address, subtotal,Email, method);
           

          var orderrepository = _unitOfWork.GetRepository<Order, Guid>();
            orderrepository.Add(order);

            var result = await _unitOfWork.SaveChanges();
            if (result <= 0)
                throw new Exception("Problem saving the order");
            return _mapper.Map<OrderResponse>(order);

        }

        private OrderItems createOrderItem(BasketItem item, Product orignalItem)
        {
           return new OrderItems
           {
               ProductId = item.Id,
               ProductName = orignalItem.Name,
               PictureUrl = orignalItem.PictureUrl,
               Price = item.Price,
               Quantity = item.Qty
           };
        }

        public Task<bool> DeleteOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
        {
           var deliveryMethod =await  _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethod);
            return result;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(string Email)
        {
           var orders =await  _unitOfWork.GetRepository<Order, Guid>().GetAllAsync( new OrderSpesifications(Email) );
            var result = _mapper.Map<IEnumerable<OrderResponse>>(orders);
            return result;
        }

        public async Task<OrderResponse> GetOrderByIdAsync(Guid id)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderSpesifications(id));
            var result = _mapper.Map<OrderResponse>(order);
            return result;
        }

        public Task<IEnumerable<OrderResponse>> GetOrdersByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
