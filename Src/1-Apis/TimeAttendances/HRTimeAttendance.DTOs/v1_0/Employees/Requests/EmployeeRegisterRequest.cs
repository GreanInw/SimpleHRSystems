using HR.Common.DTOs.HumanResources.Employees.Requests;
using HR.Common.Results;
using MediatR;

namespace HRTimeAttendance.DTOs.v1_0.Employees.Requests
{
    public class EmployeeRegisterRequest : EmployeeRegisterFromBodyBaseRequest, IRequest<ServiceResult>
    { }
}