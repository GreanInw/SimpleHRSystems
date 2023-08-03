using System.Linq.Expressions;

namespace HR.Common.EFCores.Queries
{
    public interface IAnyQuery<TEntity> where TEntity : class
    {
        bool Any(Expression<Func<TEntity, bool>> expression);
    }
}
