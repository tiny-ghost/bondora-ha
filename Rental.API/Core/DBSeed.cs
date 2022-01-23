using Rental.Core.Models;
using Rental.Persistence;

namespace Rental.API.Core
{
    internal class DBSeed
    {

        private readonly RentalContext _context;
        private readonly Customer[] _customers =
        {
            new()
            {
                Name = "Smith"
            }
        };

        private readonly Equipment[] _equipment =
        {
            new(){ Name = "Caterpillar bulldozer", Type="Heavy"},
            new(){ Name = "KamAZ truck", Type="Regular"},
            new(){ Name = "Komatsu crane", Type="Heavy"},
            new(){ Name = "Volvo steamroller", Type="Regular"},
            new(){ Name = "Bosch jackhammer", Type="Specialized"}
        };

        public DBSeed(RentalContext context)
        {
            _context = context;
        }

        public async Task Start()
        {
            await SeedCustomer();
            await SeedEquipment();
        }

        private async Task SeedCustomer()
        {

            await _context.AddRangeAsync(_customers);
            await _context.SaveChangesAsync();
        }

        private async Task SeedEquipment()
        {
            await _context.AddRangeAsync(_equipment);
            await _context.SaveChangesAsync();
        }
    }
}
