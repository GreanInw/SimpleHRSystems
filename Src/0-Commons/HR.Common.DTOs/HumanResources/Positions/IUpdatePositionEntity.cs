using HR.Common.DTOs.Bases;

namespace HR.Common.DTOs.HumanResources.Positions
{
    public interface IUpdatePositionEntity : ICreatePositionEntity, IIdBaseEntity
    {
        bool IsActive { get; set; }
    }
}
