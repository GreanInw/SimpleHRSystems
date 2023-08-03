using HR.Common.EFCores.Queries;
using System.Linq.Expressions;

namespace HR.Common.DALs.Repositories.Bases.Queries
{
    public interface IQueryRepository<TEntity> : IAnyAsyncQuery<TEntity>, IDisposable where TEntity : class
    {
        IQueryable<TEntity> All { get; }
        TEntity GetById(params object[] keyValues);
        ValueTask<TEntity> GetByIdAsync(params object[] keyValues);
        Task ReloadAsync(TEntity entity);

        ValueTask<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        ValueTask<long> LongCountAsync(Expression<Func<TEntity, bool>> expression);
    }
}