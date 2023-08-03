using HR.Common.DALs.UnitOfWorks.Bases;
using HR.Common.DbContexts.Contexts;

namespace HR.Common.DALs.UnitOfWorks
{
    public class HRUnitOfWork : UnitOfWork<IHRDbContext>, IHRUnitOfWork
    {
        public HRUnitOfWork(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}