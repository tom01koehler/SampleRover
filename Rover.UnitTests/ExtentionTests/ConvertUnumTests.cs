using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Library.Extensions;
using Rover.Library.Models.Enums;
using System.Collections.Generic;

namespace Rover.UnitTests.ExtentionTests
{
    [TestClass]
    public class ConvertUnumTests
    {

        [DataTestMethod]
        [DataRow("E", Direction.East)]
        [DataRow("e", Direction.East)]
        [DataRow("S", Direction.South)]
        [DataRow("s", Direction.South)]
        [DataRow("W", Direction.West)]
        [DataRow("w", Direction.West)]
        [DataRow("N", Direction.North)]
        [DataRow("n", Direction.North)]
        [DataRow("N ", Direction.North)] // extra space
        [DataRow("x", Direction.Invalid)]
        public void ToDirection_Enum(string inputDirection, Direction expectedResult)
        {
            // Arrange
            // Act
            var result = inputDirection.ToDirection();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [DataTestMethod]
        [DataRow(Direction.East, "E")]
        [DataRow(Direction.South, "S")]
        [DataRow(Direction.West, "W")]
        [DataRow(Direction.North, "N")]
        [DataRow(Direction.Invalid, "")]
        public void ToDirection_String(Direction enumDirection, string expectedResult)
        {
            // Arrange
            // Act
            var result = enumDirection.ToDirection();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [DataTestMethod]
        [DataRow('L', Move.Left)]
        [DataRow('l', Move.Left)]
        [DataRow('R', Move.Right)]
        [DataRow('r', Move.Right)]
        [DataRow('M', Move.Forward)]
        [DataRow('m', Move.Forward)]
        [DataRow('x', Move.Invalid)]
        public void ToMove_Enum(char moveInput, Move expectedMove)
        {
            // Assert
            // Act
            var result = moveInput.ToMove();

            // Assert
            Assert.AreEqual(expectedMove, result);
        }

        [DataTestMethod]
        [DataRow(new char[] { 'l', 'r', 'm', 'L' }, new Move[] { Move.Left, Move.Right, Move.Forward, Move.Left})]
        [DataRow(new char[] {'L' }, new Move[] { Move.Left })]
        public void ToMoves(char[] movesInput, Move[] expectedMoves)
        {
            // Arrange
            // Act
            var result = movesInput.ToMoves();

            // Assert
            CollectionAssert.AreEqual(expectedMoves, result);
        }
    }
}
