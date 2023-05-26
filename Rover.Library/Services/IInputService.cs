using Rover.Library.Models;

namespace RoverLibrary.Services
{
    public interface IInputService
    {
        void StartSimulation();
        NavigationPlan PromptUserForInput(Map map);
        void ExecuteInput(NavigationPlan navigationPlan, Map map);
        Coordinate MapCornerInput();
        Movements MovementInput();
        CoordinatePosition StartCoordinatesInput(Map map);
    }
}
