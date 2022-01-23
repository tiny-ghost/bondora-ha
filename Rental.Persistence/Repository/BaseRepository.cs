using Microsoft.EntityFrameworkCore;
using Rental.Core.Contracts;
using Rental.Core.Models;


namespace Rental.Persistence.Repository
{
    internal class BaseRepository<T>: IBaseRepository<T> where T : BaseModel, new()
    {
        private readonly RentalContext _context;

        public bool AutoSaveChanges { get; set; } = true;

        public BaseRepository(RentalContext context)
        {
            _context = context;
        }
        public virtual IQueryable<T> Units => _context.Set<T>();

        public T Add(T unit)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            _context.Entry(unit).State = EntityState.Added;

            if (AutoSaveChanges)
            {
                _context.SaveChanges();
            }

            return unit;
        }

        public async Task<T> AddAsync(T unit, CancellationToken token = default)
        {

            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            _context.Entry(unit).State = EntityState.Added;

            if (AutoSaveChanges)
            {
                await _context.SaveChangesAsync(token).ConfigureAwait(false);
            }

            return unit;

        }

        public T Get(int id)
        {
            return Units.SingleOrDefault(unit => unit.Id == id);
        }

        public async Task<T> GetAsync(int id, CancellationToken token = default)
        {
            return await Units.SingleOrDefaultAsync(unit => unit.Id == id, token);
        }

        public void Remove(int id)
        {
            var unit = Get(id);

            if (unit is null)
            {
                return;
            }

            _context.Entry(unit).State = EntityState.Deleted;

            //Faster option if entities are getting big and slow
            //Unreliable as we don't really know if entity was deleted or not
            // _context.Remove(new T{Id = id});

            if (AutoSaveChanges)
            {
                _context.SaveChanges();
            }



        }

        public async Task RemoveAsync(int id, CancellationToken token = default)
        {
            var unit = await GetAsync(id, token);

            if (unit is null)
            {
                return;
            }

            _context.Entry(unit).State = EntityState.Deleted;

            //Faster option if entities are getting big and slow
            //Unreliable as we don't really know if entity was deleted or not
            // _context.Remove(new T{Id = id});

            if (AutoSaveChanges)
            {
                await _context.SaveChangesAsync(token)
                    .ConfigureAwait(false);
            }
        }

        public void Update(T unit)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            _context.Entry(unit).State = EntityState.Modified;

            if (AutoSaveChanges)
            {
                _context.SaveChanges();
            }
        }

        public async Task UpdateAsync(T unit, CancellationToken token = default)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            _context.Entry(unit).State = EntityState.Modified;

            if (AutoSaveChanges)
            {
                await _context.SaveChangesAsync(token)
                    .ConfigureAwait(false);
            }
        }
    }
}
