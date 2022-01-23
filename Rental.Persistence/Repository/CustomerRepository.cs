using Microsoft.EntityFrameworkCore;
using Rental.Core.Contracts.Repository;
using Rental.Core.Models;


namespace Rental.Persistence.Repository
{
    internal class CustomerRepository: BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(RentalContext context):base(context)
        {

        }

        public override IQueryable<Customer> Units => base.Units.Include(c =>c.Orders);

    }
}
