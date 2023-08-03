using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.SummaryLeaves.Commands
{
    public class SummaryLeaveCommandRepository : CommandRepository<SummaryLeave, IHRDbContext>, ISummaryLeaveCommandRepository
    {
        public SummaryLeaveCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}