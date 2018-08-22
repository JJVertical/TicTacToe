using System;
using Xunit;

namespace TicTacToe.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void CtorWithNullPlayersNameShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Player(null, SpaceValue.X));
        }

        [Fact]
        public void CtorWithSpaceAvailableShouldThrowPlaysersSpaceValueAvailableException()
        {
            Assert.Throws<PlayersSpaceValueAvailableException>(() => new Player("X", SpaceValue.Available));
        }

        [Fact]
        public void CtorWithPlayerShouldSetName()
        {
            Assert.Equal("X", new Player("X", SpaceValue.X).Name);
        }

        [Fact]
        public void CtorShouldSetPlayerSpaceState()
        {
            Assert.Equal(SpaceValue.X, new Player("X", SpaceValue.X).SpaceValue);
        }

        [Fact]
        public void TotalGamesWonOnNewPlayerShouldReturn0()
        {
            Assert.Equal(0, new Player("X", SpaceValue.X).TotalGamesWon);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(20)]
        public void PlayerWonGameShouldIncrementTotalGamesWon(int numberOfGamesToWin)
        {
            // arrange
            var player = new Player("X", SpaceValue.X);

            // act
            for (var i = 0; i < numberOfGamesToWin; ++i)
            {
                player.WonGame();
            }

            // assert
            Assert.Equal(numberOfGamesToWin, player.TotalGamesWon);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(20)]
        public void ClearTotalGamesWonShouldResetTotalGamesWonToZero(int totalGamesWon)
        {
            // arrange
            var player = new Player("X", SpaceValue.X);

            for (var i = 0; i < totalGamesWon; ++i)
            {
                player.WonGame();
            }

            Assert.Equal(totalGamesWon, player.TotalGamesWon);

            // act
            player.ClearTotalGamesWon();

            // assert
            Assert.Equal(0, player.TotalGamesWon);
        }
    }
}
