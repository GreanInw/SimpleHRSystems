using HR.Common.Configurations;
using HR.Common.Constants;
using HR.Common.DALs.Repositories.HumanResources.Employees.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.HumanResources.Employees;
using HR.Common.DTOs.HumanResources.Employees.EmployeeNames;
using HR.Common.DTOs.Identities.Registers;
using HR.Common.Libs.Extensions;
using HR.Common.Libs.Providers.FormDatas;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Models.HumanResources;
using HR.Common.Results;
using HR.Common.Services.Bases;
using HRTimeAttendance.Services.Departments.Validations;
using Microsoft.AspNetCore.Http;

namespace HRTimeAttendance.Services.Employees.Commands
{
    public class EmployeeRegisterCommandService : HRCommonCommandService, IEmployeeRegisterCommandService
    {
        private readonly IFormDataFactory _formDataFactory;
        private readonly IEmployeeCommandRepository _employeeCommandRepository;
        private readonly IDepartmentValidationService _departmentValidationService;

        public EmployeeRegisterCommandService(IHttpContextAccessor httpContextAccessor
            , IHRUnitOfWork unitOfWork, IFormDataFactory formDataFactory
            , IEmployeeCommandRepository employeeCommandRepository
            , IDepartmentValidationService departmentValidationService) : base(httpContextAccessor, unitOfWork)
        {
            _formDataFactory = formDataFactory;
            _employeeCommandRepository = employeeCommandRepository;
            _departmentValidationService = departmentValidationService;
        }

        public async ValueTask<ServiceResult> RegisterAsync<TEmployeeNames>(IRegisterEntity register
            , IEmployeeEntity<TEmployeeNames> employee) where TEmployeeNames : IEmployeeNameEntity
        {
            var validateResult = await ValidateEntity(register, employee);
            if (!validateResult.IsSuccess)
            {
                return validateResult;
            }

            var result = await RegisterUser(register);
            if (!result.IsSuccess)
            {
                return result;
            }

            await CreateNewEmployeeUncommit(result.Result.ToString().ToGuid(), employee);
            try
            {
                await UnitOfWork.CommitAsync();
                return new ServiceResult(true);
            }
            catch
            {
                return new ServiceResult("Register user is complete, but cannot create employee info.");
            }
        }
        
        private async ValueTask<ServiceResult> ValidateEntity<TEmployeeNames>(IRegisterEntity register
            , IEmployeeEntity<TEmployeeNames> employee) where TEmployeeNames : IEmployeeNameEntity
        {
            if (register is null)
            {
                return new ServiceResult(ErrorMessageConstants.Identities.Users.UserEntityRequired);
            }

            if (employee is null)
            {
                return new ServiceResult(ErrorMessageConstants.HumanResources.Employees.EmployeeEntityRequired);
            }

            if (await _employeeCommandRepository.AnyAsync(w => w.InternalId == employee.InternalId))
            {
                return new ServiceResult(ErrorMessageConstants.HumanResources.Employees.InternalIdDuplicate);
            }

            return new ServiceResult(true);
        }

        private async ValueTask<ServiceResult> RegisterUser(IRegisterEntity register)
        {
            var registerUser = new RegisterUserModel
            {
                Username = register.Username,
                Password = register.Password,
                ConfirmPassword = register.ConfirmPassword
            };

            var settings = GetOptionsValueFromConfiguration<HRUrlSettings>(nameof(HRUrlSettings));
            return await _formDataFactory
                .PostAsync($"{settings.IdentityApi.Host}{settings.IdentityApi.RegisterUrl}", registerUser);
        }

        private async Task CreateNewEmployeeUncommit<TEmployeeNames>(Guid userId, IEmployeeEntity<TEmployeeNames> employee)
            where TEmployeeNames : IEmployeeNameEntity
        {
            var newEmployee = CreateEmployeeEntity(employee);
            newEmployee.UserEmployee = new UserEmployee
            {
                UserId = userId
            };
            newEmployee.EmployeeNamesInfos = employee.Names.Select(s => new EmployeeNamesInfo
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                MiddleName = s.MiddleName,
                Nickname = s.Nickname,
                LanguageId = s.LanguageId
            }).ToList();
            await _employeeCommandRepository.AddAsync(newEmployee);
        }

        private Employee CreateEmployeeEntity<TEmployeeNames>(IEmployeeEntity<TEmployeeNames> employee)
            where TEmployeeNames : IEmployeeNameEntity
            => new Employee
            {
                Id = Guid.NewGuid(),
                InternalId = employee.InternalId,
                Birthday = employee.Birthday,
                NationalId = employee.NationalId,
                Email = employee.Email,
                Nationality = employee.Nationality,
                ContactNumber = employee.ContactNumber,
                SectionId = employee.SectionId,
                DepartmentId = employee.DepartmentId,
                PositionId = employee.PositionId
            };

        public class RegisterUserModel : IRegisterEntity
        {
            [SnakeCaseFromForm(nameof(Username))]
            public string Username { get; set; }
            [SnakeCaseFromForm(nameof(Password))]
            public string Password { get; set; }
            [SnakeCaseFromForm(nameof(ConfirmPassword))]
            public string ConfirmPassword { get; set; }
        }
    }
}