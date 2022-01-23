using Rental.Core.Contracts.Service;
using Rental.Service;

namespace Rental.API.Core
{
    static class ServiceRegistrator
    {

        public static IServiceCollection Addservice(this IServiceCollection services) => services
            .AddScoped<ICustomerService,CustomerService>()
            .AddScoped<IEquipmentService,EquipmentService>()
            .AddScoped<IOrderService,OrderService>()
            .AddScoped<IInvoiceService,InvoiceService>()
            ;
    }
}
