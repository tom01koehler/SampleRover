using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Wrappers;

namespace Rover.UnitTests.WrapperTests
{
    [TestClass]
    public class ConsoleWrapperTests
    {
        private ConsoleWrapper GetTestWrapper()
        {
            return new ConsoleWrapper();
        }

        [DataTestMethod]
        [DataRow("exit", true)]
        [DataRow("e", true)]
        [DataRow("no", true)]
        [DataRow("n", true)]
        [DataRow("ExIt", true)]
        [DataRow("E", true)]
        [DataRow("NO", true)]
        [DataRow("AnyOtherText", false)]
        public void IsExit(string testInput, bool expectedResult)
        {
            // Arrange
            var testWrapper = GetTestWrapper();

            // Act
            var result = testWrapper.IsExit(testInput);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
