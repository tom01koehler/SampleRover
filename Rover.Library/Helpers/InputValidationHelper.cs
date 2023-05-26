using System.Text.RegularExpressions;

namespace Rover.Library.Helpers
{
    public class InputValidationHelper : IInputValidationHelper
    {
        private const string COORD_PATTERN = @"^[0-9]*\s+[0-9]*$"; // Any number 0-9 repeat, has space, and any number 0-9 repeat
        private const string COORD_POSITION_PATTERN = @"^[0-9]*\s+[0-9]*\s[NnEeSsWw]$"; // Any number 0-9 repeat, has space, any number 0-9 repeat, and single charcter NESW upper or lower
        private const string MOVEMENT_PATTERN = @"^[LlRrMm]*$"; // any combination of characters LRM at any length upper or lower 

        /// <summary>
        /// Checks coordinate input format ex 5 6
        /// </summary>
        /// <param name="coordinateInput"></param>
        /// <returns></returns>
        public bool CoordinateIsValid(string coordinateInput)
        {
            return RegexIsValid(COORD_PATTERN, coordinateInput.Trim());
        }

        /// <summary>
        /// Checks coordinate input format ex. 5 6 N
        /// </summary>
        /// <param name="coordinateInput"></param>
        /// <returns></returns>
        public bool CoordinatePositionIsValid(string coordinateInput)
        {
            return RegexIsValid(COORD_POSITION_PATTERN, coordinateInput.Trim());
        }

        /// <summary>
        /// Checks input is valid movement format ex. LRM
        /// </summary>
        /// <param name="movementInput"></param>
        /// <returns></returns>
        public bool MovementIsValid(string movementInput)
        {
            return RegexIsValid(MOVEMENT_PATTERN, movementInput.Trim());
        }

        /// <summary>
        /// Checks input against regex pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="input"></param>
        /// <returns>pattern match to input as bool</returns>
        public bool RegexIsValid(string pattern, string input)
        {
            var regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
    }
}
