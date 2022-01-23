
using Rental.Core.Contracts.Repository;
using Rental.Core.Models;
using System.Threading.Tasks;

namespace Rental.Core.Contracts
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }

        IEquipmentRepository Equipment { get; }

        IOrderRepository Orders { get; }

        IRentalItemRepository RentalItems { get; }
        Task<bool> CompleteAsync();
    }
}
