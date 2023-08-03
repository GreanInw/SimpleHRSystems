using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.EFCores.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.Common.DALs.Repositories.Bases.Commands
{
    public abstract class CommandRepository<TEntity, TEFCoreDbContext> : QueryRepository<TEntity, TEFCoreDbContext>, ICommandRepository<TEntity>
        where TEntity : class
        where TEFCoreDbContext : IDbContext
    {
        protected CommandRepository(TEFCoreDbContext dbContext) : base(dbContext)
        { }

        public void Add(TEntity entity) => DbSet.Add(entity);

        public async Task AddAsync(TEntity entity) => await DbSet.AddAsync(entity);

        public void AddRange(IEnumerable<TEntity> entities) => DbSet.AddRange(entities);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await DbSet.AddRangeAsync(entities);

        public void Delete(TEntity entity) => DbSet.Remove(entity);

        public void Delete(IEnumerable<TEntity> entities) => DbSet.RemoveRange(entities);

        public void Edit(TEntity entity)
        {
            DbSet.Entry(entity).State = EntityState.Modified;
        }
    }
}
