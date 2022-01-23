using Microsoft.EntityFrameworkCore;
using Rental.Core.Contracts.Repository;
using Rental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Persistence.Repository
{
    internal class EquipmentRepository:BaseRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(RentalContext context) : base(context)
        {

        }

        public override IQueryable<Equipment> Units => base.Units;

        public async Task<IEnumerable<Equipment>> GetAllAsync()
        {
            return await Units.ToListAsync();
        }

        public async Task<Equipment> GetByIdAsync(int id)
        {
            return await Units.Where(u => u.Id.Equals(id)).SingleOrDefaultAsync();
        }
    }
}
