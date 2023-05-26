namespace Rover.Library.Models
{
    public class Map
    {
        /// <summary>
        /// MapStartPoint always x = 0, y = 0
        /// </summary>
        public Coordinate MapStartPoint { get; } = new Coordinate { X = 0, Y = 0 };
        public Coordinate MapEndpoint { get; set; }
    }
}
