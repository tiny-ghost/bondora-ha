using Rental.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.Core.Contracts.Repository
{
    public interface IEquipmentRepository : IBaseRepository<Equipment>
    {
        Task<IEnumerable<Equipment>> GetAllAsync();
        Task<Equipment> GetByIdAsync(int id);
    }
}
