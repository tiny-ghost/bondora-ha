using Rental.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.Core.Contracts.Service
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> GetAllAsync();
        Task<Equipment> GetByIdAsync(int id);
    }
}
