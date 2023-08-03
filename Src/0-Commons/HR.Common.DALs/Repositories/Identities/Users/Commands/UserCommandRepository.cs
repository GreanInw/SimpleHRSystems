using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.Identities;

namespace HR.Common.DALs.Repositories.Identities.SSO.Commands
{
    public class UserCommandRepository : CommandRepository<User, IHRDbContext>, IUserCommandRepository
    {
        public UserCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}