using Microsoft.AspNetCore.Mvc;
using Rental.Core.Contracts.Service;

namespace Rental.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAsync()
        {
            var equipment = await _equipmentService.GetAllAsync();

            return Ok(equipment);
        }
    }
}
