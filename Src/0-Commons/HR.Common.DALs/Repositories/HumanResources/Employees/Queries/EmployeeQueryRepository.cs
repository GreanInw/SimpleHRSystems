using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Employees.Queries
{
    public class EmployeeQueryRepository : QueryRepository<Employee, IHRDbContext>, IEmployeeQueryRepository
    {
        public EmployeeQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}