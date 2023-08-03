using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.UserEmployees.Commands
{
    public class UserEmployeeCommandRepository : CommandRepository<UserEmployee, IHRDbContext>, IUserEmployeeCommandRepository
    {
        public UserEmployeeCommandRepository(IHRDbContext dbContext) : base(dbContext)
        {
        }
    }
}