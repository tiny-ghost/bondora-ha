using Microsoft.EntityFrameworkCore;
using Rental.Core.Models;
using Rental.Persistence;
using System.Threading.Tasks;

namespace Rental.API.Core
{
    internal class TestDBSeed
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

        public TestDBSeed(DbContextOptions<RentalContext> dbContextOptions)
        {
             _context = new RentalContext(dbContextOptions);
        }

        public void Start()
        {
           _context.Database.EnsureDeleted();
           _context.Database.EnsureCreated();

           SeedCustomer();
           SeedEquipment();
        }

        private void SeedCustomer()
        {

            _context.AddRange(_customers);
            _context.SaveChanges();
        }

        private void SeedEquipment()
        {
           _context.AddRange(_equipment);
            _context.SaveChanges();
        }
    }
}
