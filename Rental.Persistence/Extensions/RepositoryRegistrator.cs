using Microsoft.Extensions.DependencyInjection;
using Rental.Core.Contracts;

namespace Rental.Persistence.Extensions
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            ;
    }
}
