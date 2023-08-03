using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.LeaveRequests.Commands
{
    public interface ILeaveRequestCommandRepository : IQueryRepository<LeaveRequest> { }
}
