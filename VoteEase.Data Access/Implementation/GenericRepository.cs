using Microsoft.EntityFrameworkCore;
using VoteEase.Data.Context;
using VoteEase.Data_Access.Interface;

namespace VoteEase.Data_Access.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly VoteEaseDbContext dbContext;
        private DbSet<T> table;

        public GenericRepository(VoteEaseDbContext _dbContext)
        {
            dbContext = _dbContext;
            table = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> ReadAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> ReadSingle(Guid id)
        {
            return await table.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await dbContext.AddAsync(entity);
        }

        public async void Update(T entity)
        {
            var EntityModified = table.Attach(entity);
            EntityModified.State = EntityState.Modified;
        }

        public async Task Delete(Guid id)
        {
            var entity = await table.FindAsync(id);
            dbContext.Remove(entity);
        }

        public async Task<bool> SaveChanges()
        {
            return await dbContext.SaveChangesAsync() >= 0;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            table.AddRange(entities);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            table.RemoveRange(entities);
        }
    }
}
