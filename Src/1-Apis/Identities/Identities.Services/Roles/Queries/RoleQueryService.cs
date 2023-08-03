using HR.Common.DALs.Repositories.Identities.Roles.Queries;
using HR.Common.DTOs.Identities.Roles.Responses;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Identities.Services.Roles.Queries
{
    public class RoleQueryService : BaseCommonService, IRoleQueryService
    {
        public RoleQueryService(IHttpContextAccessor httpContextAccessor
            , IRoleQueryRepository roleQueryRepository) : base(httpContextAccessor)
        {
            RoleQueryRepository = roleQueryRepository;
        }

        protected IRoleQueryRepository RoleQueryRepository { get; }

        public async ValueTask<ServiceResult<IEnumerable<T>>> GetAllAsync<T>() where T : RoleBaseResponse, new()
            => new ServiceResult<IEnumerable<T>>
            {
                IsSuccess = true,
                Result = await RoleQueryRepository.All.AsNoTracking().OrderBy(o => o.Name)
                    .Select(s => new T
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        IsActive = s.IsActive,
                    }).ToListAsync()
            };
    }
}
