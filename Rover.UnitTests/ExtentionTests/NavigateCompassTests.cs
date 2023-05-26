using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Library.Extensions;
using Rover.Library.Models;
using Rover.Library.Models.Enums;

namespace Rover.UnitTests.ExtentionTests
{
    [TestClass]
    public class NavigateCompassTests
    {
        [DataTestMethod]
        [DataRow(Direction.North, Direction.West)]
        [DataRow(Direction.East, Direction.North)]
        [DataRow(Direction.South, Direction.East)]
        [DataRow(Direction.West, Direction.South)]
        [DataRow(Direction.Invalid, Direction.Invalid)]
        public void MoveLeft(Direction currentDirection, Direction expectedResult)
        {
            // Arrange
            var testPosition = new CoordinatePosition
            {
                X = 6,
                Y = 6,
                Direction = currentDirection
            };

            // Act
            testPosition.MoveLeft();

            // Assert
            Assert.AreEqual(expectedResult, testPosition.Direction);
        }

        [DataTestMethod]
        [DataRow(Direction.North, Direction.East)]
        [DataRow(Direction.East, Direction.South)]
        [DataRow(Direction.South, Direction.West)]
        [DataRow(Direction.West, Direction.North)]
        [DataRow(Direction.Invalid, Direction.Invalid)]
        public void MoveRight(Direction currentDirection, Direction expectedResult)
        {
            // Arrange
            var testPosition = new CoordinatePosition
            {
                X = 6,
                Y = 6,
                Direction = currentDirection
            };

            // Act
            testPosition.MoveRight();

            // Assert
            Assert.AreEqual(expectedResult, testPosition.Direction);
        }

        [TestMethod]
        public void MoveForward_XPlus()
        {
            // Arrange
            var expectedX = 11;
            var expectedY = 10;
            var expectedDirection = Direction.East;

            var testPosition = new CoordinatePosition
            {
                X = 10,
                Y = 10,
                Direction = Direction.East
            };

            // Act
            testPosition.MoveForward();

            // Assert
            Assert.AreEqual(expectedX, testPosition.X);
            Assert.AreEqual(expectedY, testPosition.Y);
            Assert.AreEqual(expectedDirection, expectedDirection);
        }

        [TestMethod]
        public void MoveForward_YPlus()
        {
            // Arrange
            var expectedX = 10;
            var expectedY = 11;
            var expectedDirection = Direction.North;

            var testPosition = new CoordinatePosition
            {
                X = 10,
                Y = 10,
                Direction = Direction.North
            };

            // Act
            testPosition.MoveForward();

            // Assert
            Assert.AreEqual(expectedX, testPosition.X);
            Assert.AreEqual(expectedY, testPosition.Y);
            Assert.AreEqual(expectedDirection, expectedDirection);
        }

        [TestMethod]
        public void MoveForward_XMinus()
        {
            // Arrange
            var expectedX = 9;
            var expectedY = 10;
            var expectedDirection = Direction.West;

            var testPosition = new CoordinatePosition
            {
                X = 10,
                Y = 10,
                Direction = Direction.West
            };

            // Act
            testPosition.MoveForward();

            // Assert
            Assert.AreEqual(expectedX, testPosition.X);
            Assert.AreEqual(expectedY, testPosition.Y);
            Assert.AreEqual(expectedDirection, expectedDirection);
        }

        [TestMethod]
        public void MoveForward_YMinus()
        {
            // Arrange
            var expectedX = 10;
            var expectedY = 9;
            var expectedDirection = Direction.West;

            var testPosition = new CoordinatePosition
            {
                X = 10,
                Y = 10,
                Direction = Direction.South
            };

            // Act
            testPosition.MoveForward();

            // Assert
            Assert.AreEqual(expectedX, testPosition.X);
            Assert.AreEqual(expectedY, testPosition.Y);
            Assert.AreEqual(expectedDirection, expectedDirection);
        }

        [TestMethod]
        public void MoveForward_InvalidDirection()
        {
            // Arrange
            var expectedX = 10;
            var expectedY = 10;
            var expectedDirection = Direction.Invalid;

            var testPosition = new CoordinatePosition
            {
                X = 10,
                Y = 10,
                Direction = Direction.Invalid
            };

            // Act
            testPosition.MoveForward();

            // Assert
            Assert.AreEqual(expectedX, testPosition.X);
            Assert.AreEqual(expectedY, testPosition.Y);
            Assert.AreEqual(expectedDirection, expectedDirection);
        }
    }
}
