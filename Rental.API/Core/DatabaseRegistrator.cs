using Microsoft.EntityFrameworkCore;
using Rental.Persistence;
using Rental.Persistence.Extensions;

namespace Rental.API.Core
{
    static class DatabaseRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config) => services
        .AddDbContext<RentalContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            })
            .AddTransient<DBInitialization>()
            .AddRepositories()
            ;
    }
}
