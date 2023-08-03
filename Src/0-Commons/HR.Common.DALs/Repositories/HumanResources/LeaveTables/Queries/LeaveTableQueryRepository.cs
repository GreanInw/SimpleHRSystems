using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.LeaveTables.Queries
{
    public class LeaveTableQueryRepository : QueryRepository<LeaveTable, IHRDbContext>, ILeaveTableQueryRepository
    {
        public LeaveTableQueryRepository(IHRDbContext dbContext) : base(dbContext)
        {
        }
    }
}