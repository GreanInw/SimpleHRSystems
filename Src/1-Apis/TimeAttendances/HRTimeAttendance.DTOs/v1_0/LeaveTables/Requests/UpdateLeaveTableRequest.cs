using HR.Common.DTOs.HumanResources.LeaveTables;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using MediatR;

namespace HRTimeAttendance.DTOs.v1_0.LeaveTables.Requests
{
    public class UpdateLeaveTableRequest : IUpdateLeaveTableEntity
        , IRequest<ServiceResult>
    {
        [SnakeCaseFromForm(nameof(Id))]
        public Guid Id { get; set; }

        [SnakeCaseFromForm(nameof(Name))]
        public string Name { get; set; }

        [SnakeCaseFromForm(nameof(Days))]
        public int Days { get; set; }

        [SnakeCaseFromForm(nameof(IsActive))]
        public bool IsActive { get; set; }

        [SnakeCaseFromForm(nameof(LanguageId))]
        public int LanguageId { get; set; }
    }
}