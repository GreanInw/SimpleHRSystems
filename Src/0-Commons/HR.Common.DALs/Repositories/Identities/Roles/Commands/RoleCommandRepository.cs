using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.Identities;

namespace HR.Common.DALs.Repositories.Identities.Roles.Commands
{
    public class RoleCommandRepository : CommandRepository<Role, IHRDbContext>, IRoleCommandRepository
    {
        public RoleCommandRepository(IHRDbContext dbContext) : base(dbContext) { }
    }
}