using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Departments.Queries
{
    public interface IDepartmentQueryRepository : ICommandRepository<Department> { }
}
