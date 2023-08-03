using HR.Common.EFCores.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HR.Common.DALs.Repositories.Bases.Queries
{
    public abstract class QueryRepository<TEntity, TEFCoreDbContext> : IQueryRepository<TEntity>
        where TEntity : class
        where TEFCoreDbContext : IDbContext
    {
        public QueryRepository(TEFCoreDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        protected TEFCoreDbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        public IQueryable<TEntity> All => DbSet.AsQueryable();

        public void Dispose() => DbContext.Dispose();

        public async Task ReloadAsync(TEntity entity)
            => await DbContext.Entry(entity).ReloadAsync();

        public TEntity GetById(params object[] keyValues) => DbSet.Find(keyValues);

        public async ValueTask<TEntity> GetByIdAsync(params object[] keyValues)
            => await DbSet.FindAsync(keyValues);

        public async ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
            => await All.AsNoTracking().Where(expression).Select(s => 1).AnyAsync();

        public async ValueTask<int> CountAsync(Expression<Func<TEntity, bool>> expression)
            => await All.AsNoTracking().Where(expression).Select(s => 1).CountAsync();

        public async ValueTask<long> LongCountAsync(Expression<Func<TEntity, bool>> expression)
            => await All.AsNoTracking().Where(expression).Select(s => 1).LongCountAsync();
    }
}
