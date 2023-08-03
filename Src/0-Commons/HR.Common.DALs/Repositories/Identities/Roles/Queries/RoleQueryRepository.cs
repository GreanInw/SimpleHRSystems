using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.Identities;

namespace HR.Common.DALs.Repositories.Identities.Roles.Queries
{
    public class RoleQueryRepository : QueryRepository<Role, IHRDbContext>, IRoleQueryRepository
    {
        public RoleQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}
