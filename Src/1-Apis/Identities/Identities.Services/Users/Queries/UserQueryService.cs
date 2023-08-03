using HR.Common.DALs.Repositories.Identities.SSO.Queries;
using HR.Common.DTOs.Bases;
using HR.Common.DTOs.Identities.Users;
using HR.Common.DTOs.Identities.Users.Responses;
using HR.Common.Libs.Extensions;
using HR.Common.Libs.Helpers;
using HR.Common.Models.Identities;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identities.Services.Users.Queries
{
    public class UserQueryService : BaseCommonService, IUserQueryService
    {
        public UserQueryService(IHttpContextAccessor httpContextAccessor
            , IUserQueryRepository userQueryRepository) : base(httpContextAccessor)
        {
            UserQueryRepository = userQueryRepository;
        }

        protected IUserQueryRepository UserQueryRepository { get; }

        public async ValueTask<ServiceResultPaging<T>> GetAsync<T>(IGetUsersEntity entity, IPaging paging)
            where T : UserBaseResponse, new()
        {
            var expression = GetUserExpression(entity);
            var entities = await GetUsers(expression, paging);
            return new ServiceResultPaging<T>
            {
                IsSuccess = true,
                Limit = paging.Limit,
                Result = MapToResponse<T>(entities),
                PageNumber = CommonHelpers.GetNextPageNumber(entities.Count(), paging.Limit, paging.PageNumber)
            };
        }

        private Expression<Func<User, bool>> GetUserExpression(IGetUsersEntity entity)
        {
            Expression<Func<User, bool>> expression = w => true;
            if (!entity.Username.IsEmpty())
            {
                expression = expression.And(w => w.Username.Contains(entity.Username));
            }
            if (entity.IsActive.HasValue)
            {
                expression = expression.And(w => w.IsActive == entity.IsActive.Value);
            }
            return expression;
        }

        private async ValueTask<IEnumerable<User>> GetUsers(Expression<Func<User, bool>> expression, IPaging paging)
            => await UserQueryRepository.All.AsNoTracking()
                .Include(t => t.UserInRoles).ThenInclude(t => t.Role)
                .Where(expression).OrderByDescending(w => w.Id)
                .Paging(paging.Limit, paging.PageNumber).ToListAsync();

        private IEnumerable<T> MapToResponse<T>(IEnumerable<User> users)
            where T : UserBaseResponse, new()
            => users.Select(s => new T
            {
                UserId = s.Id,
                Username = s.Username,
                IsActive = s.IsActive,
                LatestLogIn = s.LatestLogIn,
                Roles = s.UserInRoles?.Select(sr => new UserBaseResponse.RoleResponse
                {
                    Id = sr.RoleId,
                    Name = sr.Role.Name
                }) ?? new List<UserBaseResponse.RoleResponse>()
            });
    }
}