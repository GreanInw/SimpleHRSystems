using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.UserEmployees.Queries
{
    public class UserEmployeeQueryRepository : QueryRepository<UserEmployee, IHRDbContext>, IUserEmployeesQueryRepository
    {
        public UserEmployeeQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}