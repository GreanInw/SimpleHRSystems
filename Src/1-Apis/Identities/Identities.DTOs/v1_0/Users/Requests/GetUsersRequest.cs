using HR.Common.DTOs.Bases;
using HR.Common.DTOs.Identities.Users;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using Identities.DTOs.v1_0.Users.Responses;
using MediatR;

namespace Identities.DTOs.v1_0.Users.Requests
{
    public class GetUsersRequest : PagingFromQueryBase, IGetUsersEntity
        , IRequest<ServiceResultPaging<GetUsersResponse>>
    {
        [SnakeCaseFromForm(nameof(Username))]
        public string Username { get; set; }

        [SnakeCaseFromForm(nameof(IsActive))]
        public bool? IsActive { get; set; }
    }
}