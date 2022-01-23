using Rental.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Core.Contracts.Service
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByIdAsync(int Id);
        Task<Customer> GetCustomerByIdWithOrders(int id);
    }
}
