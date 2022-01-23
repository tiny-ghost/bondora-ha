using Rental.Core.Contracts;
using Rental.Core.Contracts.Repository;
using Rental.Core.Models;
using Rental.Persistence.Repository;


namespace Rental.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RentalContext _context;
        public ICustomerRepository Customers { get; private set; }
        public IEquipmentRepository Equipment { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IRentalItemRepository RentalItems { get; private set; }

        public UnitOfWork(RentalContext context)
        {
            _context = context;

            Customers = new CustomerRepository(context);
            Equipment = new EquipmentRepository(context);
            Orders = new OrderRepository(context);
            RentalItems = new RentalItemRepository(context);
        }

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
