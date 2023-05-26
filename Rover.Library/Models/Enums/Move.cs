namespace Rover.Library.Models.Enums
{
    public enum Move
    {
        /// <summary>
        /// Spin Left 90 degrees
        /// Stay at same point
        /// </summary>
        Left, 
        /// <summary>
        /// Spin Right 90 degrees
        /// Stay at same point
        /// </summary>
        Right,
        /// <summary>
        /// Move forward 1 grid point
        /// </summary>
        Forward,
        /// <summary>
        /// Not a valid Direction
        /// </summary>
        Invalid
    }
}
