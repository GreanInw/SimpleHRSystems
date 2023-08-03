using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.EmployeeNamesInfos.Queries
{
    public class EmployeeNamesInfoQueryRepository : QueryRepository<EmployeeNamesInfo, IHRDbContext>, IEmployeeNamesInfoQueryRepository
    {
        public EmployeeNamesInfoQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}