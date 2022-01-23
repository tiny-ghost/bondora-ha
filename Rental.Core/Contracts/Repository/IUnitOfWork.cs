
using Rental.Core.Contracts.Repository;
using Rental.Core.Models;

namespace Rental.Core.Contracts
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
    }
}
