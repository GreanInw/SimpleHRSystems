using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.ApprovalFlows.Commands
{
    public class ApprovalFlowCommandRepository : CommandRepository<ApprovalFlow, IHRDbContext>, IApprovalFlowCommandRepository
    {
        public ApprovalFlowCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}