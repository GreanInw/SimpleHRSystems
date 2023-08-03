using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.Identities;

namespace HR.Common.DALs.Repositories.Identities.UserInRoles.Queries
{
    public class UserInRoleQueryRepository : QueryRepository<UserInRole, IHRDbContext>
    {
        public UserInRoleQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}