

using Rental.Core.Contracts;
using Rental.Core.Contracts.Repository;
using Rental.Core.Models;

namespace Rental.Persistence.Repository
{
    internal class RentalItemRepository : BaseRepository<RentalItem>, IRentalItemRepository
    {
        public RentalItemRepository(RentalContext context) : base(context)
        {
        }

        public async Task AddRentalItemsToOrderAsync(RentalItem item)
        {

            await AddAsync(item);
        }

    }
}
