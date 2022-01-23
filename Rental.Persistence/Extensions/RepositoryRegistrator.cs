using Microsoft.Extensions.DependencyInjection;
using Rental.Core.Contracts;
using Rental.Core.Contracts.Repository;
using Rental.Core.Models;
using Rental.Persistence.Repository;


namespace Rental.Persistence.Extensions
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddTransient<ICustomerRepository, CustomerRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            ;
    }
}
