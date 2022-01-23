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

        public override IQueryable<Customer> Units => base.Units;

        public async Task<Customer> GetCustomerWithOrdersAsync(int id)
        {
            return await Units
                .Where(c => c.Id.Equals(id))
                .Include(c => c.Orders)
                .FirstOrDefaultAsync();
        }
    }
}
