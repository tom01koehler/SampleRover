using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rover.Library.Helpers;
using Rover.Library.Models;
using Rover.Library.Models.Enums;
using Rover.Library.Services;
using Rover.Services;
using Rover.Wrappers;
using System.Collections.Generic;
using System.Linq;

namespace Rover.UnitTests.ServiceTests
{
    [TestClass]
    public class InputServiceTests
    {
        #region Test Setup
        private const string TEST_INPUT = "4 4 N";
        private InputService GetTestService(Mock<IConsoleWrapper> mockConsoleWrapper = null
                                            , Mock<INavigationService> mockNavService = null
                                            , Mock<IInputValidationHelper> mockInputValidHelper = null)
        {
            mockConsoleWrapper = mockConsoleWrapper ?? GetMockConsoleWrapper();
            mockNavService = mockNavService ?? new Mock<INavigationService>();
            mockInputValidHelper = mockInputValidHelper ?? GetMockInputValidation();

            return new InputService(mockConsoleWrapper.Object, mockNavService.Object, mockInputValidHelper.Object);
        }

        private Mock<IConsoleWrapper> GetMockConsoleWrapper(string testInput = null)
        {
            testInput = testInput ?? TEST_INPUT;

            var mockConsoleWrapper = new Mock<IConsoleWrapper>();
            mockConsoleWrapper.Setup(c => c.ReadPrompt(It.IsAny<string>()))
                .Returns(testInput);
            mockConsoleWrapper.Setup(c => c.InputErrorMessage(It.IsAny<string>(), It.IsAny<string>()));
            mockConsoleWrapper.Setup(c => c.ErrorMessage(It.IsAny<string>()));
            mockConsoleWrapper.Setup(c => c.Write(It.IsAny<string>()));

            return mockConsoleWrapper;
        }

        private Mock<IInputValidationHelper> GetMockInputValidation(bool testCoordinatePosition = true
                                                                    , bool testCoordinate = true
                                                                    , bool testMovement = true)
        {
            var mockInputValidation = new Mock<IInputValidationHelper>();
            mockInputValidation.Setup(i => i.CoordinatePositionIsValid(It.IsAny<string>()))
                .Returns(testCoordinatePosition);
            mockInputValidation.Setup(i => i.CoordinateIsValid(It.IsAny<string>()))
                .Returns(testCoordinate);
            mockInputValidation.Setup(i => i.MovementIsValid(It.IsAny<string>()))
                .Returns(testMovement);

            return mockInputValidation;
        }

        private Mock<INavigationService> GetMockNavigationService(bool testOnMap = true)
        {
            var mockNavService = new Mock<INavigationService>();
            mockNavService.Setup(n => n.IsOnMap(It.IsAny<CoordinatePosition>(), It.IsAny<Map>()))
                .Returns(testOnMap);

            return mockNavService;
        }

        #endregion

        #region MapCornerInput Tests

        [TestMethod]
        public void MapCornerInput_IsValid()
        {
            // Arrange
            var testInput = "8 9";
            var expectedX = 8;
            var expectedY = 9;

            var expectedResult = new Coordinate
            {
                Input = testInput,
                X = expectedX,
                Y = expectedY
            };

            var mockConsoleWrapper = GetMockConsoleWrapper(testInput);
            var mockInputValidationHelper = GetMockInputValidation();

            var testService = GetTestService(mockConsoleWrapper, null, mockInputValidationHelper);

            // Act
            var result = testService.MapCornerInput();

            // Assert
            Assert.IsNotNull(result);
            mockInputValidationHelper.Verify(i => i.CoordinateIsValid(testInput), Times.Once);
            mockConsoleWrapper.Verify(c => c.InputErrorMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            Assert.AreEqual(expectedResult.Input, testInput);
            Assert.AreEqual(expectedY, result.Y);
            Assert.AreEqual(expectedX, result.X);
        }

        [TestMethod]
        public void MapCornerInput_IsInvalid()
        {
            // Arrange
            var testInput = "invalid-input";

            var mockConsoleWrapper = GetMockConsoleWrapper(testInput);
            var mockInputValidationHelper = GetMockInputValidation(testCoordinate: false);

            var testService = GetTestService(mockConsoleWrapper, null, mockInputValidationHelper);

            // Act
            var result = testService.MapCornerInput();

            // Assert
            Assert.IsNull(result);
            mockInputValidationHelper.Verify(i => i.CoordinateIsValid(testInput), Times.Once);
            mockConsoleWrapper.Verify(c => c.InputErrorMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region StartCoordinate Tests
        [TestMethod("Given a Map When I enter a valid input that is on the Map Then a CoordinatePosition is returned")]
        public void StartCoordinate_IsValid_IsOnMap()
        {
            // Arrange
            var testInput = "5 6 E";
            var expectedX = 5;
            var expectedY = 6;
            var expectedDirection = Direction.East;

            var expectedCoordinates = new CoordinatePosition
            {
                Input = testInput,
                X = expectedX,
                Y = expectedY,
                Direction = expectedDirection
            };

            var testMap = new Map
            {
                MapEndpoint = new Coordinate
                {
                    X = 10,
                    Y = 10
                }
            };

            var mockConsoleWrapper = GetMockConsoleWrapper(testInput);

            var mockInputValidationHelper = GetMockInputValidation();

            var mockNavigationService = GetMockNavigationService();

            var testService = GetTestService(mockConsoleWrapper, mockNavigationService, mockInputValidationHelper);

            // Act
            var result = testService.StartCoordinatesInput(testMap);

            // Assert
            Assert.IsNotNull(result);

            mockConsoleWrapper.Verify(c => c.ReadPrompt(It.IsAny<string>()), Times.Once());
            mockInputValidationHelper.Verify(i => i.CoordinatePositionIsValid(testInput), Times.Once());
            mockNavigationService.Verify(n => n.IsOnMap(It.IsAny<CoordinatePosition>(), It.IsAny<Map>()), Times.Once());
            mockConsoleWrapper.Verify(c => c.InputErrorMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

            Assert.AreEqual(expectedY, result.Y);
            Assert.AreEqual(expectedX, result.X);
            Assert.AreEqual(expectedDirection, result.Direction);
        }

        [TestMethod("Given a Map When an invalid input is entered Then the result is null")]
        public void StartCoordinate_IsNotValid()
        {
            // Arrange
            var testCoordinatePositionValid = false;
            var testInput = "invalid-input";

            var testMap = new Map
            {
                MapEndpoint = new Coordinate
                {
                    X = 10,
                    Y = 10
                }
            };

            var mockConsoleWrapper = GetMockConsoleWrapper(testInput);

            var mockInputValidationHelper = GetMockInputValidation(testCoordinatePosition:testCoordinatePositionValid);

            var mockNavigationService = GetMockNavigationService();

            var testService = GetTestService(mockConsoleWrapper, mockNavigationService, mockInputValidationHelper);

            // Act
            var result = testService.StartCoordinatesInput(testMap);

            // Arrange
            Assert.IsNull(result);
            mockConsoleWrapper.Verify(c => c.ReadPrompt(It.IsAny<string>()), Times.Once);
            mockInputValidationHelper.Verify(i => i.CoordinatePositionIsValid(testInput), Times.Once);
            mockNavigationService.Verify(n => n.IsOnMap(It.IsAny<CoordinatePosition>(), It.IsAny<Map>()), Times.Never);
            mockConsoleWrapper.Verify(c => c.InputErrorMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod("Given a Map When an Valid input is entered that is not on the Map Then the result is null")]
        public void StartCoordinate_IsNotOnMap()
        {
            // Arrange
            var testOnMapValid = false;

            var testInput = "100 200 N";

            var testMap = new Map
            {
                MapEndpoint = new Coordinate
                {
                    X = 10,
                    Y = 10
                }
            };

            var mockConsoleWrapper = GetMockConsoleWrapper(testInput);

            var mockInputValidationHelper = GetMockInputValidation();

            var mockNavigationService = GetMockNavigationService(testOnMap:testOnMapValid);

            var testService = GetTestService(mockConsoleWrapper, mockNavigationService, mockInputValidationHelper);

            // Act
            var result = testService.StartCoordinatesInput(testMap);

            // Assert
            Assert.IsNull(result);

            mockConsoleWrapper.Verify(c => c.ReadPrompt(It.IsAny<string>()), Times.Once);
            mockInputValidationHelper.Verify(i => i.CoordinatePositionIsValid(testInput), Times.Once);
            mockNavigationService.Verify(n => n.IsOnMap(It.IsAny<CoordinatePosition>(), It.IsAny<Map>()), Times.Once);
            mockConsoleWrapper.Verify(c => c.InputErrorMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
        #endregion

        #region MovementInput Tests

        [TestMethod]
        public void MovementInput_IsValid()
        {
            // Arrange
            var testInput = "LRML";
            var expectedMovement = new Movements
            {
                Input = testInput,
                Moves = new List<Move> { Move.Left, Move.Right, Move.Forward, Move.Left }
            };

            var mockConsoleWrapper = GetMockConsoleWrapper(testInput);
            var mockInputValidationHelper = GetMockInputValidation();

            var testService = GetTestService(mockConsoleWrapper, null, mockInputValidationHelper);

            // Act
            var result = testService.MovementInput();

            // Assert
            Assert.IsNotNull(result);
            mockConsoleWrapper.Verify(c => c.ReadPrompt(It.IsAny<string>()), Times.Once);
            mockConsoleWrapper.Verify(c => c.InputErrorMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            mockInputValidationHelper.Verify(i => i.MovementIsValid(It.IsAny<string>()), Times.Once);

            Assert.AreEqual(testInput, result.Input);
            CollectionAssert.AreEqual(expectedMovement.Moves.ToArray(), result.Moves.ToArray());
        }


        [TestMethod]
        public void MovementInput_IsInvalid()
        {
            // Arrange
            var testMovementValid = false;

            var testInput = "invalid-input";

            var mockConsoleWrapper = GetMockConsoleWrapper(testInput);
            var mockInputValidationHelper = GetMockInputValidation(testMovement: testMovementValid);

            var testService = GetTestService(mockConsoleWrapper, null, mockInputValidationHelper);

            // Act
            var result = testService.MovementInput();

            // Assert
            Assert.IsNull(result);
            mockConsoleWrapper.Verify(c => c.ReadPrompt(It.IsAny<string>()), Times.Once);
            mockConsoleWrapper.Verify(c => c.InputErrorMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockInputValidationHelper.Verify(i => i.MovementIsValid(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region ExecuteInput Tests

        [TestMethod]
        public void ExecuteInput_IsOnMap_Success()
        {
            // Arrange
            var testNewPosition = new CoordinatePosition { X = 5, Y = 5, Direction = Direction.South };
            var testPlan = new NavigationPlan
            {
                CurrentPosition = new CoordinatePosition { X = 3, Y = 3, Direction = Direction.North },
                Movements = new Movements
                {
                    Moves = new List<Move> { Move.Left, Move.Right, Move.Forward }
                }
            };

            var testMap = new Map();

            var mockConsoleWrapper = GetMockConsoleWrapper();
            var mockNavigationService = GetMockNavigationService();
            mockNavigationService.Setup(n => n.NavigateMove(It.IsAny<CoordinatePosition>(), It.IsAny<Move>()))
                .Returns(testNewPosition);

            var testService = GetTestService(mockConsoleWrapper, mockNavigationService);

            // Act
            testService.ExecuteInput(testPlan, testMap);

            // Assert
            mockNavigationService.Verify(n => n.IsOnMap(It.IsAny<CoordinatePosition>(), It.IsAny<Map>()), Times.Exactly(3));
            mockNavigationService.Verify(n => n.NavigateMove(It.IsAny<CoordinatePosition>(), It.IsAny<Move>()), Times.Exactly(3));
            mockConsoleWrapper.Verify(c => c.ErrorMessage(It.IsAny<string>()), Times.Never);
            mockConsoleWrapper.Verify(c => c.Write(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ExecuteInput_IsNotOnMap_Error()
        {
            // Arrange
            var testNewPosition = new CoordinatePosition { X = 5, Y = 5, Direction = Direction.South };
            var testPlan = new NavigationPlan
            {
                CurrentPosition = new CoordinatePosition { X = 3, Y = 3, Direction = Direction.North },
                Movements = new Movements
                {
                    Moves = new List<Move> { Move.Left, Move.Right, Move.Forward }
                }
            };

            var testMap = new Map();

            var mockConsoleWrapper = GetMockConsoleWrapper();
            var mockNavigationService = GetMockNavigationService(testOnMap: false);
            mockNavigationService.Setup(n => n.NavigateMove(It.IsAny<CoordinatePosition>(), It.IsAny<Move>()))
                .Returns(testNewPosition);

            var testService = GetTestService(mockConsoleWrapper, mockNavigationService);

            // Act
            testService.ExecuteInput(testPlan, testMap);

            // Assert
            mockNavigationService.Verify(n => n.IsOnMap(It.IsAny<CoordinatePosition>(), It.IsAny<Map>()), Times.Once);
            mockNavigationService.Verify(n => n.NavigateMove(It.IsAny<CoordinatePosition>(), It.IsAny<Move>()), Times.Once);
            mockConsoleWrapper.Verify(c => c.ErrorMessage(It.IsAny<string>()), Times.Exactly(2));
            mockConsoleWrapper.Verify(c => c.Write(It.IsAny<string>()), Times.Never);
        }

        #endregion
    }
}
