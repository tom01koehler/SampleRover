using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Library.Helpers;

namespace Rover.UnitTests.HelperTests
{
    [TestClass]
    public class InputValidationHelperTests
    {
        private InputValidationHelper GetTestHelper()
        {
            return new InputValidationHelper();
        }

        [DataTestMethod]
        [DataRow("1 2", true)]
        [DataRow("13 5", true)]
        [DataRow("103 200", true)]
        [DataRow("1", false)]
        [DataRow("", false)]
        public void CoordinateIsValid(string coordinates, bool expectedSuccess)
        {
            // Arrange
            var testHelper = GetTestHelper();

            // Act
            var result = testHelper.CoordinateIsValid(coordinates);

            // Assert
            Assert.AreEqual(expectedSuccess, result);
        }

        [DataTestMethod]
        [DataRow("1 2 E", true)]
        [DataRow("13 5 S", true)]
        [DataRow("103 200 W", true)]
        [DataRow("1 5 N", true)]
        [DataRow("4 3 e ", true)]
        [DataRow("10 0 n", true)]
        [DataRow("20 1 s", true)]
        [DataRow("22 30 w", true)]
        [DataRow("1", false)]
        [DataRow("N", false)]
        [DataRow("10 12", false)]
        [DataRow("11 15 t", false)]
        public void CoordinatePositionIsValid(string coordinates, bool expectedSuccess)
        {
            // Arrange
            var testHelper = GetTestHelper();

            // Act
            var result = testHelper.CoordinatePositionIsValid(coordinates);

            // Assert
            Assert.AreEqual(expectedSuccess, result);
        }

        [DataTestMethod]
        [DataRow("L", true)]
        [DataRow("R", true)]
        [DataRow("M", true)]
        [DataRow("LLRMM", true)]
        [DataRow("lrm", true)]
        [DataRow("LRMx", false)]
        public void MovementIsValid(string instructions, bool expectedSuccess)
        {
            // Arrange
            var testHelper = GetTestHelper();

            // Act
            var result = testHelper.MovementIsValid(instructions);

            // Assert
            Assert.AreEqual(expectedSuccess, result);
        }
    }
}
