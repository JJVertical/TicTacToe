using System;
using TicTacToe.UI.ViewModels;
using Xunit;

namespace TicTacToe.UI.Tests
{
    public class GameSizeListItemTests
    {
        [Fact]
        public void CtorNullDisplayTextShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GameSizeListItem(null, 3));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void CtorInvalidBoardSizeShouldThrowArgumentOutOfRangeException(int boardSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameSizeListItem("Text Text", boardSize));
        }
    }
}
