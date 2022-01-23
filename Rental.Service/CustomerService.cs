using Rental.Core.Contracts;
using Rental.Core.Contracts.Service;
using Rental.Core.Models;

namespace Rental.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Customer> GetCustomerByIdAsync(int Id)
        {
            return await _unitOfWork.Customers.GetAsync(Id);
        }

        public async Task<Customer> GetCustomerByIdWithOrders(int id)
        {
            return await _unitOfWork.Customers.GetCustomerWithOrdersAsync(id);
        }
    }
}
