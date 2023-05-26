using Rover.Library.Extensions;
using Rover.Library.Helpers;
using Rover.Library.Models;
using Rover.Library.Services;
using Rover.Wrappers;
using RoverLibrary.Services;
using System;
using System.Linq;

namespace Rover.Services
{
    public class InputService : IInputService
    {
        private readonly IConsoleWrapper _consoleWrapper;
        private readonly INavigationService _navigationService;
        private readonly IInputValidationHelper _inputValidationHelper;

        private int _instructionCount = 1;

        private const string PROMPT_INSTR = "Follow prompts to input a navigation plan, enter 'Exit' [E] to end the simulation...";
        private const string PROMPT_CONTINUE = "Would you like to add another navigation plan? Enter 'Yes' [Y] to continue; 'No' [N] or 'Exit' to end...";
        private const string PROMPT_PREFIX = "Rover";
        private const string PROMPT_ENTER_CORNER = "Enter Graph Upper Right Coordinate: ";
        private const string PROMPT_START_POS = "Starting Position: ";
        private const string PROMPT_START_COORD = "Start Coordinates";
        private const string PROMPT_MAP_CORNER = "Map Corner";
        private const string PROMPT_MVMT_PLN = "Movement Plan: ";
        private const string PROMPT_MVMT = "Movements";
        private const string PROMPT_OUT = "Output: ";

        private const string PROMPT_HELPER_COORD = "x y coordinate ex. 5 6";
        private const string PROMPT_HELPER_COORD_POS = "x y coordinate with direction ex. 5 6 N";
        private const string PROMPT_HELPER_MOVE = "movement direction (Left, Right, Move) ex. LRM in any length or order";

        public InputService(IConsoleWrapper consoleWrapper
                            , INavigationService navigationService
                            , IInputValidationHelper inputValidationHelper)
        {
            _consoleWrapper = consoleWrapper;
            _navigationService = navigationService;
            _inputValidationHelper = inputValidationHelper;
        }

        /// <summary>
        /// Starts simulation to begin process of prompting user for input to begin building Map
        /// </summary>
        public void StartSimulation()
        {
            _consoleWrapper.Write(PROMPT_INSTR);

            var map = new Map();

            do
            {
                map.MapEndpoint = MapCornerInput();
            }
            while (map.MapEndpoint is null);
            

            do
            {
                var plan = PromptUserForInput(map);
                ExecuteInput(plan, map);
                _instructionCount++;
            } 
            while(!_consoleWrapper.IsExit(_consoleWrapper.ReadPrompt(PROMPT_CONTINUE))); // Repeat until exit
        }

        /// <summary>
        /// Begins building navigation plan
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public NavigationPlan PromptUserForInput(Map map)
        {
            var navPlan = new NavigationPlan();

            do
            {
                navPlan.StartPoint = StartCoordinatesInput(map);
            } while (navPlan.StartPoint is null); // Repeat until valid input

            do
            {
                navPlan.Movements = MovementInput();
            } while (navPlan.Movements is null); // Repeat until valid input
            

            return navPlan;
        }

        /// <summary>
        /// Collects user input for Map upper right corner
        /// </summary>
        /// <returns></returns>
        public Coordinate MapCornerInput()
        {
            var coordInput = _consoleWrapper.ReadPrompt(PROMPT_ENTER_CORNER);

            if (_inputValidationHelper.CoordinateIsValid(coordInput))
            {
                var coordArr = coordInput.Split(' ').Select(Int32.Parse).ToArray();
                var coordinates = new Coordinate
                {
                    Input = coordInput,
                    X = coordArr[0],
                    Y = coordArr[1]
                };
                if(coordinates.X > 0 && coordinates.Y > 0)
                    return coordinates;
            } 

            _consoleWrapper.InputErrorMessage(PROMPT_MAP_CORNER, PROMPT_HELPER_COORD);
            return null;
        }

        /// <summary>
        /// Prompts user for starting coordinates 
        /// </summary>
        /// <param name="map"></param>
        /// <returns>CoordinatePosition</returns>
        public CoordinatePosition StartCoordinatesInput(Map map)
        {
            var startCoordinatePrompt = $"{PROMPT_PREFIX} {_instructionCount} {PROMPT_START_POS}";

            var coordInput = _consoleWrapper.ReadPrompt(startCoordinatePrompt);

            if(_inputValidationHelper.CoordinatePositionIsValid(coordInput))
            {
                var coordArr = coordInput.Split(' ').Select(c => c.Trim()).ToArray();

                var coordinates = new CoordinatePosition
                {
                    Input = coordInput,
                    X = Int32.Parse(coordArr[0]),
                    Y = Int32.Parse(coordArr[1]),
                    Direction = coordArr[2].ToDirection()
                };

                if(_navigationService.IsOnMap(coordinates, map))
                    return coordinates;
            } 
            
            _consoleWrapper.InputErrorMessage(PROMPT_START_COORD, PROMPT_HELPER_COORD_POS);
            return null;
        }

        /// <summary>
        /// Collects user input for Movements on map
        /// </summary>
        /// <returns></returns>
        public Movements MovementInput()
        {
            var movementPrompt = $"{PROMPT_PREFIX} {_instructionCount} {PROMPT_MVMT_PLN}";

            var movementInput = _consoleWrapper.ReadPrompt(movementPrompt);

            if(_inputValidationHelper.MovementIsValid(movementInput))
            {
                return new Movements
                {
                    Input = movementInput,
                    Moves = movementInput.ToCharArray().ToMoves()
                };
            } 

            _consoleWrapper.InputErrorMessage(PROMPT_MVMT, PROMPT_HELPER_MOVE);
            return null;
        }

        /// <summary>
        /// Executes entire navigation plan, confirms each movement is on Map
        /// </summary>
        /// <param name="navPlan"></param>
        /// <param name="map"></param>
        public void ExecuteInput(NavigationPlan navPlan, Map map)
        {
            var successfulNavigation = true;

            foreach(var move in navPlan.Movements.Moves)
            {
                navPlan.CurrentPosition = _navigationService.NavigateMove(navPlan.CurrentPosition, move);
                if(!_navigationService.IsOnMap(navPlan.CurrentPosition,map))
                {
                    _consoleWrapper.ErrorMessage($"Navigation plan moved {PROMPT_PREFIX} outside of map coordinates at position {navPlan.CurrentPosition.X} {navPlan.CurrentPosition.Y}");
                    successfulNavigation = false;
                    break;
                }
            }
            
            if(successfulNavigation)
            {
                _consoleWrapper.Write($"{PROMPT_PREFIX} {_instructionCount} {PROMPT_OUT}{navPlan.CurrentPosition.X} {navPlan.CurrentPosition.Y} {navPlan.CurrentPosition.Direction.ToDirection()}");
            } else
            {
                _consoleWrapper.ErrorMessage($"Navigation failed for Navigation Plan {navPlan.Movements.Input}");
            }
        }
    }
}
