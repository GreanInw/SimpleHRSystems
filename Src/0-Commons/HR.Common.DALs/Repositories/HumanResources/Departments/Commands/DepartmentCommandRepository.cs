using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Departments.Commands
{
    public class DepartmentCommandRepository : CommandRepository<Department, IHRDbContext>, IDepartmentCommandRepository
    {
        public DepartmentCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}