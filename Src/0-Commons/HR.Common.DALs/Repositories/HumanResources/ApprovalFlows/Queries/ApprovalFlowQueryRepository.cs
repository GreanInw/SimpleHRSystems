using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.ApprovalFlows.Queries
{
    public class ApprovalFlowQueryRepository : QueryRepository<ApprovalFlow, IHRDbContext>, IApprovalFlowQueryRepository
    {
        public ApprovalFlowQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}