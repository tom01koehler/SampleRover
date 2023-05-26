using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rover.Library.Helpers;
using Rover.Library.Services;
using Rover.Services;
using Rover.Wrappers;

namespace Rover.UnitTests
{
    [TestClass]
    public class UserFlowTests
    {
        private const string PROMPT_ENTER_CORNER = "Enter Graph Upper Right Coordinate: ";
        private const string PROMPT_START_POS = "Starting Position: ";
        private const string PROMPT_MVMT_PLN = "Movement Plan: ";

        [TestMethod("Given a console application When two navigation plans are ented in sequence Then the ouput for each rover is correct")]
        [Priority(1)]
        public void RoverFlow()
        {
            var mockConsoleWrapper = new Mock<IConsoleWrapper>();
            mockConsoleWrapper.Setup(c => c.Write(It.IsAny<string>()));
            mockConsoleWrapper.Setup(c => c.ErrorMessage(It.IsAny<string>()));
            mockConsoleWrapper.Setup(c => c.InputErrorMessage(It.IsAny<string>(), It.IsAny<string>()));

            mockConsoleWrapper.Setup(c => c.ReadPrompt(PROMPT_ENTER_CORNER))
                .Returns("5 5");
            mockConsoleWrapper.SetupSequence(c => c.ReadPrompt(It.Is<string>(s => s.Contains(PROMPT_START_POS))))
                .Returns("1 2 N")
                .Returns("3 3 E");
            mockConsoleWrapper.SetupSequence(c => c.ReadPrompt(It.Is<string>(s => s.Contains(PROMPT_MVMT_PLN))))
                .Returns("LMLMLMLMM")
                .Returns("MMRMMRMRRM");
            mockConsoleWrapper.SetupSequence(c => c.IsExit(It.IsAny<string>()))
                .Returns(false)
                .Returns(true);

            var navService = new NavigationService();
            var validationHelper = new InputValidationHelper();

            var inputService = new InputService(mockConsoleWrapper.Object, navService, validationHelper);
            inputService.StartSimulation();


            mockConsoleWrapper.Verify(c => c.ReadPrompt(PROMPT_ENTER_CORNER), Times.Once);
            mockConsoleWrapper.Verify(c => c.ReadPrompt("Enter Graph Upper Right Coordinate: "), Times.Once);
            mockConsoleWrapper.Verify(c => c.ReadPrompt("Rover 1 Starting Position: "), Times.Once);
            mockConsoleWrapper.Verify(c => c.Write("Rover 1 Output: 1 3 N"), Times.Once);

            mockConsoleWrapper.Verify(c => c.ReadPrompt("Rover 2 Starting Position: "), Times.Once);
            mockConsoleWrapper.Verify(c => c.Write("Rover 2 Output: 5 1 E"), Times.Once);

            mockConsoleWrapper.Verify(c => c.ErrorMessage(It.IsAny<string>()), Times.Never);
            mockConsoleWrapper.Verify(c => c.InputErrorMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
