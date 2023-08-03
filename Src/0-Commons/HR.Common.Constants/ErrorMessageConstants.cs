namespace HR.Common.Constants
{
    public class ErrorMessageConstants
    {
        public class Identities
        {
            public class Users
            {
                public const string InvalidUsernameOrPassword = "Username or password is not match.";
                public const string UserInactive = "User inactive.";
                public const string UserNotFound = "User not found.";
                public const string OldPasswordInvalid = "Old password invalid.";
                public const string UserEntityRequired = "User entity is required.";
            }

            public class Roles
            {
                public const string RoleNotFound = "Role not found.";
                public const string NameDuplicate = "Role name : '{0}' is duplicate.";
            }
        }

        public class HumanResources
        {
            public class LeaveTables
            {
                public const string LeaveNotFound = "Leave master data not found.";
                public const string NameDuplicate = "Name : '{0}' is duplicate.";
            }

            public class Holidays
            {
                public const string HolidayNotFound = "Holiday not found.";
                public const string DateDuplicate = "Date : '{0}' is duplidate.";
            }

            public class Departments
            {
                public const string DepartmentNotFound = "Department data not found.";
                public const string DepartmentDuplicate = "Department : '{0}' is duplicate.";
                public const string ParentIdNotFound = "Parent of department not found.";
            }

            public class Positions
            {
                public const string PositionNotFound = "Position data not found.";
                public const string PositionDuplicate = "Position : '{0}' is duplicate.";
            }

            public class Employees
            {
                public const string EmployeeEntityRequired = "Employee entity is required.";
                public const string InternalIdDuplicate = "Internal id is duplicate.";
            }
        }

        public class Commons
        {
            public class Languages
            {
                public const string LanguageNotFound = "Language not found or not config.";
            }
        }
    }
}
