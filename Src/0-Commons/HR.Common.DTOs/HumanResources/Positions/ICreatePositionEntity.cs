using HR.Common.DTOs.Languages;

namespace HR.Common.DTOs.HumanResources.Positions
{
    public interface ICreatePositionEntity : ILanguageIdEntity
    {
        string Name { get; set; }
    }
}