using Rover.Library.Extensions;
using Rover.Library.Models;
using Rover.Library.Models.Enums;

namespace Rover.Library.Services
{
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Checks if Coordinate is more than 0,0 and less than upper corner of map
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public bool IsOnMap(Coordinate coordinates, Map map)
        {
            return coordinates.X <= map.MapEndpoint.X && coordinates.Y <= map.MapEndpoint.Y // Inside Endpoint
                && coordinates.X >= map.MapStartPoint.X && coordinates.Y >= map.MapStartPoint.Y; // Non negative
        }

        /// <summary>
        /// Performs navigation from current position using instruction
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="moveInstruction"></param>
        /// <returns></returns>
        public CoordinatePosition NavigateMove(CoordinatePosition currentPosition, Move moveInstruction)
        {
            switch(moveInstruction)
            {
                case Move.Left:
                    currentPosition.MoveLeft();
                    break;
                case Move.Right:
                    currentPosition.MoveRight();
                    break;
                case Move.Forward:
                    currentPosition.MoveForward();
                    break;
            }
            return currentPosition;
        }
    }
}
