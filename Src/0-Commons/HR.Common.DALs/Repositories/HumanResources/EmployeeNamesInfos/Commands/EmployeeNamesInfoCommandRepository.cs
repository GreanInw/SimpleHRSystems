using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.EmployeeNamesInfos.Commands
{
    public class EmployeeNamesInfoCommandRepository : CommandRepository<EmployeeNamesInfo, IHRDbContext>, IEmployeeNamesInfoCommandRepository
    {
        public EmployeeNamesInfoCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}