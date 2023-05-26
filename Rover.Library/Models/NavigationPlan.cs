namespace Rover.Library.Models
{
    public class NavigationPlan
    {
        private CoordinatePosition _startPoint;

        /// <summary>
        /// Set starting point resets Current position
        /// </summary>
        public CoordinatePosition StartPoint {
            get { return _startPoint; }
            set
            {
                _startPoint = value;
                CurrentPosition = _startPoint;
            }
        }
        public CoordinatePosition CurrentPosition { get; set; }
        public Movements Movements { get; set; }
    }
}
