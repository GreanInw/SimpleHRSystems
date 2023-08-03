using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.Models.Identities;

namespace HR.Common.DALs.Repositories.Identities.SSO.Commands
{
    public interface IUserCommandRepository : ICommandRepository<User> { }
}
