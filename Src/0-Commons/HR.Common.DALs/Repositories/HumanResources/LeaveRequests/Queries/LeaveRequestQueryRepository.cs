using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.LeaveRequests.Queries
{
    public class LeaveRequestQueryRepository : QueryRepository<LeaveRequest, IHRDbContext>, ILeaveRequestQueryRepository
    {
        public LeaveRequestQueryRepository(IHRDbContext dbContext) : base(dbContext)
        {
        }
    }
}