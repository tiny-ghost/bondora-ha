using Microsoft.AspNetCore.Mvc;
using Rental.Core.Contracts.Service;
using Rental.Core.Models;

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

            var customerModel = new CustomerViewModel
            {
                Id = customer.Id,

                Name = customer.Name
            };

            return Ok(customerModel);
        }
    }
    //For simplicity and time saving viewmodels stored with controller and mapping is done manually
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }

    
}
