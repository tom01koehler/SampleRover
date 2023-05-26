using Rover.Library.Models.Enums;
using System.Collections.Generic;

namespace Rover.Library.Models
{
    public class Movements : InputValue
    {
        public IList<Move> Moves { get; set; }
    }
}
