using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Positions.Commands
{
    public interface IPositionCommandRepository : ICommandRepository<Position>
    { }
}