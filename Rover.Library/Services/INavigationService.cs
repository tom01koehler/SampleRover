using Rover.Library.Models;
using Rover.Library.Models.Enums;

namespace Rover.Library.Services
{
    public interface INavigationService
    {
        CoordinatePosition NavigateMove(CoordinatePosition currentPosition, Move move);
        bool IsOnMap(Coordinate coordinates, Map map);
    }
}
