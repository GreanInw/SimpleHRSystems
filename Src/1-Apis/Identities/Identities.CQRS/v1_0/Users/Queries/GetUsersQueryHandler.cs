using HR.Common.Results;
using Identities.DTOs.v1_0.Users.Requests;
using Identities.DTOs.v1_0.Users.Responses;
using Identities.Services.Users.Queries;
using MediatR;

namespace Identities.CQRS.v1_0.Users.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersRequest, ServiceResultPaging<GetUsersResponse>>
    {
        private readonly IUserQueryService _userQueryService;

        public GetUsersQueryHandler(IUserQueryService userQueryService)
        {
            _userQueryService = userQueryService;
        }

        public async Task<ServiceResultPaging<GetUsersResponse>> Handle(GetUsersRequest request
            , CancellationToken cancellationToken)
            => await _userQueryService.GetAsync<GetUsersResponse>(request, request);
    }
}
