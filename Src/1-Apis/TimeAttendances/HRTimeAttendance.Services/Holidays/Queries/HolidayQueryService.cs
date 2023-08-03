using HR.Common.DALs.Repositories.HumanResources.Holidays.Queries;
using HR.Common.DTOs.HumanResources.Holidays;
using HR.Common.DTOs.HumanResources.Holidays.Responses;
using HR.Common.Libs.Extensions;
using HR.Common.Models.HumanResources;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HRTimeAttendance.Services.Holidays.Queries
{
    public class HolidayQueryService : BaseCommonService, IHolidayQueryService
    {
        public HolidayQueryService(IHttpContextAccessor httpContextAccessor
             , IHolidayQueryRepository holidayQueryRepository) : base(httpContextAccessor)
        {
            HolidayQueryRepository = holidayQueryRepository;
        }

        protected IHolidayQueryRepository HolidayQueryRepository { get; }

        public async ValueTask<ServiceResult<IEnumerable<T>>> GetAsync<T>(IGetHolidayEntity entity) where T : HolidayBaseResponse, new()
        {
            var expression = GetExpression(entity);
            var entities = await HolidayQueryRepository.All.AsNoTracking()
                .Where(expression).ToListAsync();

            return new ServiceResult<IEnumerable<T>>
            {
                IsSuccess = true,
                Result = entities.Select(s => new T
                {
                    Id = s.Id,
                    Name = s.Name,
                    Date = s.Date,
                    IsActive = s.IsActive,
                    LanguageId = s.LanguageId
                })
            };
        }

        private Expression<Func<Holiday, bool>> GetExpression(IGetHolidayEntity entity)
        {
            int year = entity.Year ?? DateTime.UtcNow.Year;
            Expression<Func<Holiday, bool>> expression = w => w.Date.Year == year;

            if (entity.LanguageId.HasValue)
            {
                expression = expression.And(w => w.LanguageId == entity.LanguageId);
            }

            return expression;
        }
    }
}
