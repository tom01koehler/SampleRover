using Rover.Library.Models;
using Rover.Library.Models.Enums;

namespace Rover.Library.Extensions
{
    public static class NavigateCompass
    {
        /// <summary>
        /// Updates CoordinatePosition to Left movement enum.Direction
        /// </summary>
        /// <param name="currentPosition"></param>
        public static void MoveLeft(this CoordinatePosition currentPosition)
        {
            switch (currentPosition.Direction)
            {
                case Direction.North:
                    currentPosition.Direction = Direction.West;
                    break;
                case Direction.East:
                    currentPosition.Direction = Direction.North;
                    break;
                case Direction.South:
                    currentPosition.Direction = Direction.East;
                    break;
                case Direction.West:
                    currentPosition.Direction = Direction.South;
                    break;
            }
        }

        /// <summary>
        /// Updates CoordinatePosition to Right movement enum.Direction
        /// </summary>
        /// <param name="currentPosition"></param>
        public static void MoveRight(this CoordinatePosition currentPosition)
        {
            switch (currentPosition.Direction)
            {
                case Direction.North:
                    currentPosition.Direction = Direction.East;
                    break;
                case Direction.East:
                    currentPosition.Direction = Direction.South;
                    break;
                case Direction.South:
                    currentPosition.Direction = Direction.West;
                    break;
                case Direction.West:
                    currentPosition.Direction = Direction.North;
                    break;
            }
        }

        /// <summary>
        /// Updates CoordinatePosition x or y to movement +/- 
        /// </summary>
        /// <param name="currentPosition"></param>
        public static void MoveForward(this CoordinatePosition currentPosition)
        {
            switch (currentPosition.Direction)
            {
                case Direction.North:
                    currentPosition.Y++;
                    break;
                case Direction.East:
                    currentPosition.X++;
                    break;
                case Direction.South:
                    currentPosition.Y--;
                    break;
                case Direction.West:
                    currentPosition.X--;
                    break;
            }
        }
    }
}
