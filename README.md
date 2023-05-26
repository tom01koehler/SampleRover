# Rover

## Use Case
A squad of robotic rovers are to be landed by NASA on a plateau on Mars.  The navigation team needs a utility for them to simulate rover movements so they can develop a navigation plan.

A rover's position is represented by a combination of an x and y co-ordinates and a letter
representing one of the four cardinal compass points. The plateau is divided up into a grid to
simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom
left corner and facing North.

In order to control a rover, NASA sends a simple string of letters. The possible letters are:

'L' – Make the rover spin 90 degrees left without moving from its current spot\
'R' - Make the rover spin 90 degrees right without moving from its current spot\
'M'. Move forward one grid point, and maintain the same heading.

Assume that the square directly North from (x, y) is (x, y+1).

## Inputs
The first line of input is the upper-right coordinates of the plateau, the lower-left coordinates are
assumed to be 0,0.

The rest of the input is information pertaining to the rovers that have been deployed. Each rover
has two lines of input. The first line gives the rover's position, and the second line is a series of
instructions telling the rover how to explore the plateau.

The position is made up of two integers and a letter separated by spaces, corresponding to the x
and y co-ordinates and the rover's orientation.

Each rover will be finished sequentially, which means that the second rover won't start to move
until the first one has finished moving.

## Sample Output
**Example Program Flow:**\
Enter Graph Upper Right Coordinate: 5 5\
Rover 1 Starting Position: 1 2 N\
Rover 1 Movement Plan: LMLMLMLMM\
Rover 1 Output: 1 3 N\
Rover 2 Starting Position: 3 3 E\
Rover 2 Movement Plan: MMRMMRMRRM\
Rover 2 Output: 5 1 E

## Asumptions
### UI
- Basic UX to ease user input
- Simple validation and messaging to helper user with input

### Flow
- Repeat n times from user prompt to continue

### Testing
- Unit testing framework noted, preferred over scripting inputs for test 

## Specification
- C# Console Application
- .NET 5.0
- MSTest Unit Tests with Moq

## Approach
- Use DI to separate implementation from definition, mockable interfaces for testing
- Separate library for core functionality, separation allows for different implementations with minimal duplication (i.e. API, Function or Web UI instead of Console App).
- High percentage code coverage but focus on logic over coverage for value of tests
