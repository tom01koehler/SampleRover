using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Library.Models;
using Rover.Library.Models.Enums;

namespace Rover.UnitTests.ModelTests
{
    [TestClass]
    public class NavigationPlanTests
    {
        [TestMethod]
        public void Set_StartPoint_Sets_CurrentPosition()
        {
            // Arrange
            var testModel = new NavigationPlan();
            var testStartPosition = new CoordinatePosition
            {
                X = 100,
                Y = 200,
                Direction = Direction.North
            };

            // Act
            testModel.StartPoint = testStartPosition;

            // Assert
            Assert.AreEqual(testStartPosition, testModel.StartPoint);
            Assert.AreEqual(testStartPosition, testModel.CurrentPosition);
        }
    }
}
