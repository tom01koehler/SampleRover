using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Library.Models;

namespace Rover.UnitTests.ModelTests
{
    [TestClass]
    public class MapTests
    {
        [TestMethod("Given a Map When it's Initialized Then MapStartPoint is initialized as point 0,0")]
        public void Map_Initialize()
        {
            // Arrange
            // Act
            var testModel = new Map();

            // Assert
            Assert.IsNotNull(testModel.MapStartPoint);
            Assert.IsNull(testModel.MapEndpoint);
            Assert.AreEqual(0, testModel.MapStartPoint.X);
            Assert.AreEqual(0, testModel.MapStartPoint.Y);
        }
    }
}
