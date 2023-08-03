using HR.Common.DTOs.HumanResources.Departments;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Departments.Responses;
using MediatR;

namespace HRTimeAttendance.DTOs.v1_0.Departments.Requests
{
    public class GetDepartmentRequest : IGetDepartmentEntity, IRequest<ServiceResult<IEnumerable<GetDepartmentResponse>>>
    {
        [SnakeCaseFromQuery(nameof(ParentId))]
        public Guid? ParentId { get; set; }

        [SnakeCaseFromQuery(nameof(LanguageId))]
        public int? LanguageId { get; set; }
    }
}
