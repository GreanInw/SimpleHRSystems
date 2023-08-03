using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.LeaveTables.Commands
{
    public class LeaveTableCommandRepository : CommandRepository<LeaveTable, IHRDbContext>, ILeaveTableCommandRepository
    {
        public LeaveTableCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}