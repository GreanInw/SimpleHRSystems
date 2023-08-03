using HR.Common.DALs.Repositories.HumanResources.Departments.Queries;
using HR.Common.DTOs.HumanResources.Departments;
using HR.Common.DTOs.HumanResources.Departments.Responses;
using HR.Common.Libs.Extensions;
using HR.Common.Models.HumanResources;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HRTimeAttendance.Services.Departments.Queries
{
    public class DepartmentQueryService : BaseCommonService, IDepartmentQueryService
    {
        public DepartmentQueryService(IHttpContextAccessor httpContextAccessor
            , IDepartmentQueryRepository departmentQueryRepository) : base(httpContextAccessor)
        {
            DepartmentQueryRepository = departmentQueryRepository;
        }

        protected IDepartmentQueryRepository DepartmentQueryRepository { get; }

        public async ValueTask<ServiceResult<IEnumerable<T>>> GetAsync<T>(IGetDepartmentEntity entity)
            where T : DepartmentBaseResponse, new()
        {
            var expression = GetExpression(entity);
            var entities = await DepartmentQueryRepository.All
                .AsNoTracking().Where(expression).ToListAsync();

            return new ServiceResult<IEnumerable<T>>
            {
                IsSuccess = true,
                Result = entities.Select(s => new T
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsActive = s.IsActive,
                    LanguageId = s.LanguageId,
                    ParentId = s.ParentId
                })
            };
        }

        private Expression<Func<Department, bool>> GetExpression(IGetDepartmentEntity entity)
        {
            Expression<Func<Department, bool>> expression = w => true;
            if (entity.ParentId.HasValue)
            {
                expression = expression.And(w => w.ParentId == entity.ParentId);
            }

            if (entity.LanguageId.HasValue)
            {
                expression = expression.And(w => w.LanguageId == entity.LanguageId.Value);
            }

            return expression;
        }
    }
}