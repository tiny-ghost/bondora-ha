using Rental.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.Core.Contracts.Service
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderForCustomerASync(Order order);
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersByCustomerIdAsync(int customerId);
    }
}
