using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rental.Core.Contracts.Service;

namespace Rental.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            return Ok(customer);    
        }
        [HttpGet("{id}/orders")]

        public async Task<IActionResult> GetCustomerByIdWithOrdersASync(int id)
        {
            var customer = await _customerService.GetCustomerByIdWithOrders(id);

            return Ok(customer);
        }
    }
}
