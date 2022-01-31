using Microsoft.AspNetCore.Mvc;
using Rental.Core.Contracts.Service;
using Rental.Core.ViewModel;

namespace Rental.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IOrderService orderService, IInvoiceService invoiceService)
        {
            _orderService = orderService;
            _invoiceService = invoiceService;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetInvoiceAsync(int orderId)
        {
            try
            {

           
            var result = new FileStreamResult(fileStream: await _invoiceService.CreateTextInvoiceFromOrderAsync(orderId), "text/plain")
            {
                FileDownloadName = $"Order_number_{orderId}_invoice.txt"
            };

            return result;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
 }
