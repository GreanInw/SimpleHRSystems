using HR.Common.DALs.Repositories.HumanResources.Positions.Queries;
using HR.Common.DTOs.HumanResources.Positions.Responses;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HRTimeAttendance.Services.Positions.Queries
{
    public class PositionQueryService : BaseCommonService, IPositionQueryService
    {
        public PositionQueryService(IHttpContextAccessor httpContextAccessor
            , IPositionQueryRepository positionQueryRepository) : base(httpContextAccessor)
        {
            PositionQueryRepository = positionQueryRepository;
        }

        protected IPositionQueryRepository PositionQueryRepository { get; }

        public async ValueTask<ServiceResult<IEnumerable<T>>> GetAsync<T>() where T : PositionBaseResponse, new()
        {
            var entities = await PositionQueryRepository.All.AsNoTracking().ToListAsync();
            return new ServiceResult<IEnumerable<T>>
            {
                IsSuccess = true,
                Result = entities.Select(s => new T
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsActive = s.IsActive,
                    LanguageId = s.LanguageId
                })
            };
        }
    }
}