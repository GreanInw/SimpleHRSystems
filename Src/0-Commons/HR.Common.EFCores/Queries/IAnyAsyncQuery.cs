using System.Linq.Expressions;

namespace HR.Common.EFCores.Queries
{
    public interface IAnyAsyncQuery<TEntity> where TEntity : class
    {
        ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
    }
}
