using System;
using System.Collections.Generic;
using Xunit;

namespace TicTacToe.Tests
{
    /// <summary>
    /// Tests for GameBoard
    /// </summary>
    public class GameBoardTests : BaseGameBoardTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CtorBoardSizeLessThanOneShouldThrowArgumentOutOfRangeException(int boardSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameBoard(boardSize));
        }

        [Fact]
        public void CtorShouldIntializeEmptyBoard()
        {
            Assert.True(new GameBoard(DefaultTestBoardSize).BoardIsEmpty());
        }

        [Theory, MemberData(nameof(InvalidCoordinatesTestData))]
        public void TakeSpaceWithInvalidCoordinateShouldThrowArgumentOutOfRangeException(int x, int y)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameBoard(DefaultTestBoardSize).TakeSpace(SpaceValue.X, x, y));
        }

        [Theory, MemberData(nameof(InvalidCoordinatesTestData))]
        public void SpaceAvailableWithInvalidCoordinateShouldThrowArgumentOutOfRangeException(int x, int y)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameBoard(DefaultTestBoardSize).SpaceAvailable(x, y));
        }

        [Theory, MemberData(nameof(InvalidCoordinatesTestData))]
        public void GetSpaceValueWithInvalidCoordinateShouldThrowArgumentOutOfRangeException(int x, int y)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameBoard(DefaultTestBoardSize).GetSpaceValue(x, y));
        }

        public static IEnumerable<object[]> InvalidCoordinatesTestData() => new[]
        {
            new object[] { 0, -1 }, // Y less than zero
            new object[] { 0, 3 }, // Y greater then board length
            new object[] { -1, 0 }, // X less than zero
            new object[] { 3, 0 } // X greater then board width
        };

        [Fact]
        public void BoardIsEmptyShouldReturnTrueForEmptyBoard()
        {
            Assert.True(new GameBoard(DefaultTestBoardSize).BoardIsEmpty());
        }

        [Theory, MemberData(nameof(SpaceTestData))]
        public void BoardIsEmptyShouldReturnFalseForNonEmptyBoard(SpaceValue spaceValue, int x, int y)
        {
            Assert.False(new GameBoard(DefaultTestBoardSize).TakeSpace(spaceValue, x, y).BoardIsEmpty());
        }

        [Theory, MemberData(nameof(SpaceTestData))]
        public void TakeEmptySpaceShouldTakeSpace(SpaceValue spaceValue, int x, int y)
        {
            Assert.Equal(spaceValue, new GameBoard(DefaultTestBoardSize).TakeSpace(spaceValue, x, y).GetSpaceValue(x, y));
        }

        [Theory, MemberData(nameof(SpaceTestData))]
        public void TakeNonAvailableSpaceShouldThrowSpaceNotAvailableException(SpaceValue spaceValue, int x, int y)
        {
            Assert.Throws<SpaceNotAvailableException>(() => new GameBoard(DefaultTestBoardSize).TakeSpace(spaceValue, x, y).TakeSpace(spaceValue, x, y));
        }

        [Theory, MemberData(nameof(SpaceTestData))]
        public void SpaceAvailableOnNonAvailableSpaceShouldReturnFalse(SpaceValue spaceValue, int x, int y)
        {
            Assert.False(new GameBoard(DefaultTestBoardSize).TakeSpace(spaceValue, x, y).SpaceAvailable(x, y));
        }

        [Theory, MemberData(nameof(SpaceTestData))]
        public void GetSpaceValueShouldReturnSpaceValue(SpaceValue spaceValue, int x, int y)
        {
            Assert.False(new GameBoard(DefaultTestBoardSize).TakeSpace(spaceValue, x, y).SpaceAvailable(x, y));
        }

        public static IEnumerable<object[]> SpaceTestData() => new[]
{
            new object[] { SpaceValue.O, 0, 0 }, // left upper
            new object[] { SpaceValue.O, 0, 1 }, // left middle
            new object[] { SpaceValue.X, 0, 2 }, // left bottom
            new object[] { SpaceValue.O, 1, 0 }, // center upper
            new object[] { SpaceValue.X, 1, 1 }, // center center
            new object[] { SpaceValue.O, 1, 2 }, // center bottom
            new object[] { SpaceValue.X, 2, 0 }, // right upper
            new object[] { SpaceValue.O, 2, 1 }, // right center
            new object[] { SpaceValue.O, 2, 2 }, // right bottom
        };

        [Theory, MemberData(nameof(AvailableSpaceTestData))]
        public void GetSpaceValueOnAvailableSpaceSpaceShouldReturnAvailable(int x, int y)
        {
            Assert.Equal(SpaceValue.Available, new GameBoard(DefaultTestBoardSize).GetSpaceValue(x, y));
        }

        [Theory, MemberData(nameof(AvailableSpaceTestData))]
        public void SpaceAvailableOnAvailableSpaceShouldReturnTrue(int x, int y)
        {
            Assert.True(new GameBoard(DefaultTestBoardSize).SpaceAvailable(x, y));
        }

        public static IEnumerable<object[]> AvailableSpaceTestData() => new[]
        {
            new object[] { 0, 0 }, // left upper
            new object[] { 0, 1 }, // left middle
            new object[] { 0, 2 }, // left bottom
            new object[] { 1, 0 }, // center upper
            new object[] { 1, 1 }, // center center
            new object[] { 1, 2 }, // center bottom
            new object[] { 2, 0 }, // right upper
            new object[] { 2, 1 }, // right center
            new object[] { 2, 2 }, // right bottom
        };

        [Fact]
        public void BoardIsFullShouldReturnFalseForEmptyBoard()
        {
            Assert.False(new GameBoard(DefaultTestBoardSize).BoardIsFull());
        }

        [Fact]
        public void BoardIsFullShouldReturnFalseForNonFullBoard()
        {
            Assert.False(new GameBoard(DefaultTestBoardSize).TakeSpace(SpaceValue.X, 1, 1).BoardIsFull());
        }

        [Fact]
        public void BoardIsFullShouldReturnTrueForFullBoard()
        {
            Assert.True(new GameBoard(DefaultTestBoardSize)
            .TakeSpace(SpaceValue.X, 0, 0)
            .TakeSpace(SpaceValue.O, 0, 1)
            .TakeSpace(SpaceValue.O, 0, 2)
            .TakeSpace(SpaceValue.O, 1, 0)
            .TakeSpace(SpaceValue.X, 1, 1)
            .TakeSpace(SpaceValue.X, 1, 2)
            .TakeSpace(SpaceValue.X, 2, 0)
            .TakeSpace(SpaceValue.O, 2, 1)
            .TakeSpace(SpaceValue.O, 2, 2)
            .BoardIsFull());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void CreateDifferentSizeBoardsCreatesBoards(int boardSize)
        {
            // arrange
            var endCornerLocation = boardSize - 1;

            // act
            var gameBoard = new GameBoard(boardSize).TakeSpace(SpaceValue.X, endCornerLocation, endCornerLocation);

            // assert
            Assert.Equal(boardSize, gameBoard.BoardSize);
            Assert.Equal(SpaceValue.X, gameBoard.GetSpaceValue(endCornerLocation, endCornerLocation));
        }
    }
}
