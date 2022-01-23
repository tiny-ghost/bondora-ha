using Rental.Core.Contracts;
using Rental.Core.Contracts.Service;
using Rental.Core.Models;

namespace Rental.Service
{
    public class EquipmentService : IEquipmentService
    {

        private readonly IUnitOfWork _unitOfWork;

        public EquipmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Equipment>> GetAllAsync()
        {
            return await _unitOfWork.Equipment.GetAllAsync();
        }

        public async Task<Equipment> GetByIdAsync(int id)
        {
           return await _unitOfWork.Equipment.GetByIdAsync(id);
        }
    }
}
