using HR.Common.DTOs.Identities.Registers;
using Newtonsoft.Json;

namespace HR.Common.DTOs.HumanResources.Employees.Requests
{
    [JsonObject]
    public class EmployeeRegisterFromBodyBaseRequest
    {
        public RegisterUserRequest Register { get; set; }
        public EmployeeFromBodyBaseRequest Employee { get; set; }

        public class RegisterUserRequest : IRegisterEntity
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}