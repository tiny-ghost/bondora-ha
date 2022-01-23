using Rental.Core.ViewModel;
using System.Threading.Tasks;

namespace Rental.Core.Contracts.Service
{
    public interface IInvoiceService
    {
        Task<InvoiceViewModel> CreateInvoiceFromOrderAsync(int orderId);
    }
}
