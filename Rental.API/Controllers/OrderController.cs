using Microsoft.AspNetCore.Mvc;
using Rental.Core.Contracts.Service;
using Rental.Core.Models;

namespace Rental.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IEquipmentService _equipmentService;

        public OrderController(IOrderService orderService, IEquipmentService equipmentService, ICustomerService customerService)
        {
            _orderService = orderService;
            _equipmentService = equipmentService;
            _customerService = customerService;
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return  Ok(await _orderService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrderForCustomer([FromBody] OrderViewModel model)
        {

            var order = new Order()
            {
                CustomerId = model.CustomerId,
                RentItems = model.RentalItems,
            };

           

            return Ok(await _orderService.PlaceOrderForCustomerASync(order));
        }
    }
    //For simplicity and time saving viewmodels stored with controller and mapping is done manually
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EquipmentId { get; set; }
        public int DaysOfRent { get; set; }
        public List<RentalItem> RentalItems { get; set; }
    }
}
