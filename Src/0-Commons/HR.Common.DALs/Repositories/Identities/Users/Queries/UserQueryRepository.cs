using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.Identities;

namespace HR.Common.DALs.Repositories.Identities.SSO.Queries
{
    public class UserQueryRepository : QueryRepository<User, IHRDbContext>, IUserQueryRepository
    {
        public UserQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}