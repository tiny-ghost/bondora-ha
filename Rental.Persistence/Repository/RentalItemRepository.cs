using Rental.Core.Contracts.Repository;
using Rental.Core.Models;

namespace Rental.Persistence.Repository
{
    internal class RentalItemRepository : BaseRepository<RentalItem>, IRentalItemRepository
    {
        public RentalItemRepository(RentalContext context) : base(context)
        {
        }

        public override IQueryable<RentalItem> Units => base.Units;
    }
}
