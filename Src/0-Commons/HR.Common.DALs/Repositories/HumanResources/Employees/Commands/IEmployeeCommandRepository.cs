using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Employees.Commands
{
    public interface IEmployeeCommandRepository : ICommandRepository<Employee> { }
}
