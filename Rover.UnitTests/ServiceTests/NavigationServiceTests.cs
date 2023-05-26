using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Library.Models;
using Rover.Library.Models.Enums;
using Rover.Library.Services;

namespace Rover.UnitTests.ServiceTests
{
    [TestClass]
    public class NavigationServiceTests
    {
        private NavigationService GetTestService()
        {
            return new NavigationService();
        }

        #region IsOnMap Tests
        [TestMethod]
        public void IsOnMap_True()
        {
            // Arrange
            var testCoord = new Coordinate
            {
                X = 3,
                Y = 4,
            };
            var testMap = new Map
            {
                MapEndpoint = new Coordinate
                {
                    X = 10,
                    Y = 10
                }
            };

            var testService = GetTestService();

            // Act
            var result = testService.IsOnMap(testCoord, testMap);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsOnMap_Fail_X()
        {
            // Arrange
            var testCoord = new Coordinate
            {
                X = 5,
                Y = 4,
            };
            var testMap = new Map
            {
                MapEndpoint = new Coordinate
                {
                    X = 4,
                    Y = 10
                }
            };

            var testService = GetTestService();

            // Act
            var result = testService.IsOnMap(testCoord, testMap);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsOnMap_Fail_Y()
        {
            // Arrange
            var testCoord = new Coordinate
            {
                X = 8,
                Y = 8,
            };
            var testMap = new Map
            {
                MapEndpoint = new Coordinate
                {
                    X = 4,
                    Y = 6
                }
            };

            var testService = GetTestService();

            // Act
            var result = testService.IsOnMap(testCoord, testMap);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsOnMap_Fail_X_Negative()
        {
            // Arrange
            var testCoord = new Coordinate
            {
                X = -5,
                Y = 4,
            };
            var testMap = new Map
            {
                MapEndpoint = new Coordinate
                {
                    X = 4,
                    Y = 10
                }
            };

            var testService = GetTestService();

            // Act
            var result = testService.IsOnMap(testCoord, testMap);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsOnMap_Fail_Y_Negative()
        {
            // Arrange
            var testCoord = new Coordinate
            {
                X = 5,
                Y = -4,
            };
            var testMap = new Map
            {
                MapEndpoint = new Coordinate
                {
                    X = 6,
                    Y = 6
                }
            };

            var testService = GetTestService();

            // Act
            var result = testService.IsOnMap(testCoord, testMap);

            // Assert
            Assert.IsFalse(result);
        }
        #endregion

        #region NavigateMove Tests

        [TestMethod]
        public void NavigateMove_Left()
        {
            // Arrange
            var expectedDirection = Direction.West;
            var expectedX = 10;
            var expectedY = 10;

            var testMove = Move.Left;
            var testCoord = new CoordinatePosition
            {
                X = 10,
                Y = 10,
                Direction = Direction.North
            };

            var testService = GetTestService();

            // Act
            var result = testService.NavigateMove(testCoord, testMove);

            // Assert
            Assert.AreEqual(expectedDirection, result.Direction);
            Assert.AreEqual(expectedY, result.Y);
            Assert.AreEqual(expectedX, result.X);
        }

        [TestMethod]
        public void NavigateMove_Right()
        {
            // Arrange
            var expectedDirection = Direction.East;
            var expectedX = 10;
            var expectedY = 10;

            var testMove = Move.Right;
            var testCoord = new CoordinatePosition
            {
                X = 10,
                Y = 10,
                Direction = Direction.North
            };

            var testService = GetTestService();

            // Act
            var result = testService.NavigateMove(testCoord, testMove);

            // Assert
            Assert.AreEqual(expectedDirection, result.Direction);
            Assert.AreEqual(expectedY, result.Y);
            Assert.AreEqual(expectedX, result.X);
        }

        [TestMethod]
        public void NavigateMove_Forward()
        {
            // Arrange
            var expectedDirection = Direction.North;
            var expectedX = 10;
            var expectedY = 11;

            var testMove = Move.Forward;
            var testCoord = new CoordinatePosition
            {
                X = 10,
                Y = 10,
                Direction = Direction.North
            };

            var testService = GetTestService();

            // Act
            var result = testService.NavigateMove(testCoord, testMove);

            // Assert
            Assert.AreEqual(expectedDirection, result.Direction);
            Assert.AreEqual(expectedY, result.Y);
            Assert.AreEqual(expectedX, result.X);
        }

        #endregion
    }
}
