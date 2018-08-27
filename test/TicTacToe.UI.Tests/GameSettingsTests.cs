using System;
using TicTacToe.UI.ViewModels;
using Xunit;

namespace TicTacToe.UI.Tests
{
    public class GameSettingsTests
    {
        [Fact]
        public void CtorNullPlayer1ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GameSettings(null, "Player2", 3));
        }

        [Fact]
        public void CtorNullPlayer2ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GameSettings("Player1", null, 3));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void CtorInvalidBoardSizeShouldThrowArgumentOutOfRangeException(int boardSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameSettings("Player1", "Player2", boardSize));
        }
    }
}
