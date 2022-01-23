using Microsoft.EntityFrameworkCore;
using Rental.Core.Contracts.Repository;
using Rental.Core.Models;

namespace Rental.Persistence.Repository
{
    internal class OrderRepository:BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(RentalContext context) : base(context)
        {

        }
        public override IQueryable<Order> Units => base.Units;


        //Has to be covered by test valid data must be guaranteed.
        //Non zero renting days, not empty rental items etc.
        public async Task<Order> GetByIdASync(int id)
        {
             return await Units
                .Include(o => o.RentItems)
                .ThenInclude(c => c.Equipment)
                .Where(o => o.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {

            return await AddAsync(order);
        }

    }
}
