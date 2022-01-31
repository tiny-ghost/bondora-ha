using Rental.Core.ViewModel;
using System.IO;
using System.Threading.Tasks;

namespace Rental.Core.Contracts.Service
{
    public interface IInvoiceService
    {
        Task<MemoryStream> CreateTextInvoiceFromOrderAsync(int orderId);
    }
}
