using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.LeaveRequests.Commands
{
    public class LeaveRequestCommandRepository : QueryRepository<LeaveRequest, IHRDbContext>, ILeaveRequestCommandRepository
    {
        public LeaveRequestCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}