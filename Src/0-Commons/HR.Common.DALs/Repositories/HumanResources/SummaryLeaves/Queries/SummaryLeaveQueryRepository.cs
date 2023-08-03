using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.SummaryLeaves.Queries
{
    public class SummaryLeaveQueryRepository : QueryRepository<SummaryLeave, IHRDbContext>
    {
        public SummaryLeaveQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}