using HR.Common.DALs.Repositories.Identities.SSO.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.Models.Identities;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Identities.Services.Users.Bases
{
    public abstract class UserCommandBaseService : BaseCommonCommandService<IHRUnitOfWork>
    {
        protected UserCommandBaseService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork
            , IUserCommandRepository userCommandRepository)
            : base(httpContextAccessor, unitOfWork)
        {
            UserCommandRepository = userCommandRepository;
        }

        protected IUserCommandRepository UserCommandRepository { get; }

        protected async ValueTask<User> GetUserAsync(string username)
            => await UserCommandRepository.All.FirstOrDefaultAsync(w => w.Username == username);

        protected async ValueTask<User> GetUserAsync(Guid userId, bool isUpdate, bool includeRole = false)
        {
            var query = UserCommandRepository.All;
            if (!isUpdate)
            {
                query = query.AsNoTracking();
            }

            if (includeRole)
            {
                query = query.Include(t => t.UserInRoles).ThenInclude(t => t.Role);
            }

            return await query.FirstOrDefaultAsync(w => w.Id == userId);
        }
    }
}