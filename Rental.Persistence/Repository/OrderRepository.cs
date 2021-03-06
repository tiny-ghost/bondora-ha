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

        public async Task<Order> GetByIdASync(int id)
        {
             return await Units
                .Include(o => o.RentItems)
                .ThenInclude(c => c.Equipment)
                .Where(o => o.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetAllByCustomerAsync(int customerId)
        {
            return await Units
                .Where(o => o.CustomerId.Equals(customerId))
                .Include(o => o.RentItems)
                .ThenInclude(o => o.Equipment)
                .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {

            return await AddAsync(order);
        }

    }
}
