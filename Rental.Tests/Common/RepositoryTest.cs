using Microsoft.EntityFrameworkCore;
using Rental.API.Core;
using Rental.Persistence;
using System.Threading.Tasks;

namespace Rentlat.Tests
{
    public abstract class RepositoryTest
    {
        protected DbContextOptions<RentalContext> ContextOptions { get; }
        private readonly TestDBSeed _seed;

            public  RepositoryTest(DbContextOptions<RentalContext> contextOptions)
        {
            ContextOptions = contextOptions;
            using var context = new RentalContext(contextOptions);
            _seed = new TestDBSeed(contextOptions);
            _seed.Start();


        }
    }
}
