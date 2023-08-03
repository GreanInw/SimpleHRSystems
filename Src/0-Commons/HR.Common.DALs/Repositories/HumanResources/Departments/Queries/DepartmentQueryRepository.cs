using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Departments.Queries
{
    public class DepartmentQueryRepository : CommandRepository<Department, IHRDbContext>, IDepartmentQueryRepository
    {
        public DepartmentQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}