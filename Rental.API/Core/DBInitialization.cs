using Microsoft.EntityFrameworkCore;
using Rental.Persistence;

namespace Rental.API.Core
{
    public class DBInitialization
    {
        private readonly RentalContext _context;
        private readonly DBSeed _seed;

        public DBInitialization(RentalContext context)
        {
            _context = context;
            _seed = new DBSeed(context);
        }

        public async Task InitializeAsync()
        {
            await _context.Database.EnsureDeletedAsync().ConfigureAwait(false);
            await _context.Database.MigrateAsync();
            await _seed.Start().ConfigureAwait(false);
        }

    }
}
