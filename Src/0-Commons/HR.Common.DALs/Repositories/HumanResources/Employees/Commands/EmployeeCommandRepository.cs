using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Employees.Commands
{
    public class EmployeeCommandRepository : CommandRepository<Employee, IHRDbContext>, IEmployeeCommandRepository
    {
        public EmployeeCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}