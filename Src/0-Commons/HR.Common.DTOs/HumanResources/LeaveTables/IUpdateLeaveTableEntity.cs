
namespace HR.Common.DTOs.HumanResources.LeaveTables
{
    public interface IUpdateLeaveTableEntity : ICreateLeaveTableEntity
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
    }
}