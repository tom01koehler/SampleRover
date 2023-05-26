using Rover.Library.Models.Enums;
using System.Linq;

namespace Rover.Library.Extensions
{
    public static class ConvertEnum
    {
        /// <summary>
        /// Input direction returns enum
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>enum.Direction</returns>
        public static Direction ToDirection(this string direction)
        {
            var value = Direction.Invalid;

            switch(direction.Trim().ToUpper())
            {
                case "E":
                    value = Direction.East;
                    break;
                case "S":
                    value = Direction.South;
                    break;
                case "W":
                    value = Direction.West;
                    break;
                case "N":
                    value = Direction.North;
                    break;
            }

            return value;
        }

        /// <summary>
        /// Input enum.Direction returns text value
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>string direction</returns>
        public static string ToDirection(this Direction direction)
        {
            var value = string.Empty;

            switch(direction)
            {
                case Direction.East:
                    value = "E";
                    break;
                case Direction.South:
                    value = "S";
                    break;
                case Direction.West:
                    value = "W";
                    break;
                case Direction.North:
                    value = "N";
                    break;
            }

            return value;
        }

        /// <summary>
        /// Inputs char[] of Moves and returns enum.Move[]
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        public static Move[] ToMoves(this char[] moves)
        {
            Move[] values;

            values = moves.Select(m => m.ToMove()).ToArray();

            return values;
        }

        /// <summary>
        /// Input char move and returns enum.Move
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public static Move ToMove(this char move)
        {
            var value = Move.Invalid;

            switch(move.ToString().ToUpper())
            {
                case "L":
                    value = Move.Left;
                    break;
                case "R":
                    value = Move.Right;
                    break;
                case "M":
                    value = Move.Forward;
                    break;
            }

            return value;
        }
    }
}
