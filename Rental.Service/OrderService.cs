using Rental.Core.Contracts;
using Rental.Core.Contracts.Service;
using Rental.Core.Models;

namespace Rental.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> GetByIdAsync(int id)
        {
             return await _unitOfWork.Orders.GetByIdASync(id);
        }
        public async Task<Order> PlaceOrderForCustomerASync(Order order)
        {
            await _unitOfWork.Orders.CreateOrderAsync(order);

            foreach (var item in order.RentItems)
            {
                await _unitOfWork.RentalItems.AddAsync(item);
            }
            
            
            return order;

        }

        public async Task<IEnumerable<Order>> GetAllCustomerOrdersAsync(int customerId)
        {
           return await _unitOfWork.Orders.GetAllByCustomerAsync(customerId);
        }
    }
}
