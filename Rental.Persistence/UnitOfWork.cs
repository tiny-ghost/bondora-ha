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

        public UnitOfWork(RentalContext context)
        {
            _context = context;

            Customers = new CustomerRepository(context);
        }

        public async Task<bool> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
