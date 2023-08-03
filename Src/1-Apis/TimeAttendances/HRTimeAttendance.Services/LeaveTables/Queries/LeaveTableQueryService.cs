using HR.Common.DALs.Repositories.HumanResources.LeaveTables.Queries;
using HR.Common.DTOs.HumanResources.LeaveTables.Responses;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HRTimeAttendance.Services.LeaveTables.Queries
{
    public class LeaveTableQueryService : BaseCommonService, ILeaveTableQueryService
    {
        public LeaveTableQueryService(IHttpContextAccessor httpContextAccessor
            , ILeaveTableQueryRepository leaveTableQueryRepository) : base(httpContextAccessor)
        {
            LeaveTableQueryRepository = leaveTableQueryRepository;
        }

        protected ILeaveTableQueryRepository LeaveTableQueryRepository { get; }

        public async ValueTask<ServiceResult<IEnumerable<T>>> GetAllAsync<T>()
            where T : LeaveTableBaseResponse, new()
        {
            var query = LeaveTableQueryRepository.All.AsNoTracking();
            if (CurrentLanguageId > 0)
            {
                query = query.Where(w => w.LanguageId == CurrentLanguageId);
            }
            return new ServiceResult<IEnumerable<T>>
            {
                IsSuccess = true,
                Result = await query.Select(s => new T
                {
                    Id = s.Id,
                    Days = s.Days,
                    Name = s.Name,
                    IsActive = s.IsActive,
                    LanguageId = s.LanguageId,
                }).ToListAsync()
            };
        }
    }
}
