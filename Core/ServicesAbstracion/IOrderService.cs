using Shared.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstracion
{
   public interface IOrderService
    {
        Task<OrderResponse> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderResponse>> GetOrdersByUserIdAsync(string userId);
        Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(string Email);
        Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest,string Email);
        //Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<bool> DeleteOrderAsync(int orderId);

        Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync();

    }
}
