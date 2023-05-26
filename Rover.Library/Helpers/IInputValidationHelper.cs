namespace Rover.Library.Helpers
{
    public interface IInputValidationHelper
    {
        bool CoordinateIsValid(string coordinateInput);
        bool CoordinatePositionIsValid(string coordinateInput);
        bool MovementIsValid(string movementInput);

        bool RegexIsValid(string pattern, string input);
    }
}
