using Rental.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Core.Contracts.Repository
{
    public interface IOrderRepository :  IBaseRepository<Order>
    {
        Task<Order> GetByIdASync(int id);
        Task<Order> CreateOrderAsync(Order order);

    }
}
