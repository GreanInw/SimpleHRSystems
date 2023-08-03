using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.Identities;

namespace HR.Common.DALs.Repositories.Identities.UserInRoles.Commands
{
    public class UserInRoleCommandRepository : CommandRepository<UserInRole, IHRDbContext>, IUserInRoleCommandRepository
    {
        public UserInRoleCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}