using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rental.Core.Contracts
{
    public interface IBaseRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Units { get; }
        T Add(T unit);
        T Get(int id);
        void Update(T unit);
        void Remove(int id);
        Task<T> AddAsync(T unit, CancellationToken token = default);
        Task<T> GetAsync(int id, CancellationToken token = default);
        Task UpdateAsync(T unit, CancellationToken token = default);
        Task RemoveAsync(int id, CancellationToken token = default);
    }
}
