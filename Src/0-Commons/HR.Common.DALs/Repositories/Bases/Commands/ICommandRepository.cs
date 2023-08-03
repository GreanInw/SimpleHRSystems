using HR.Common.DALs.Repositories.Bases.Queries;

namespace HR.Common.DALs.Repositories.Bases.Commands
{
    public interface ICommandRepository<TEntity> : IQueryRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Edit(TEntity entity);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
    }
}
