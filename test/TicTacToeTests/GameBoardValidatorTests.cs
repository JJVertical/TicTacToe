using System;
using System.Collections.Generic;
using Xunit;

namespace TicTacToe.Tests
{
    /// <summary>
    /// Tests for GameBoardValidator
    /// </summary>
    public class GameBoardValidatorTests : BaseGameBoardTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        public void CtorWithInvalidBoardSizeThrowsArgumentOutOfRangeException(int boardSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameBoardValidator(boardSize));
        }

        [Fact]
        public void ValidateMoveWithNullOriginalBoardThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GameBoardValidator(DefaultTestBoardSize).MoveWasValid(null, new GameBoard(DefaultTestBoardSize)));
        }

        [Fact]
        public void ValidateMoveWithNullChangedBoardThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GameBoardValidator(DefaultTestBoardSize).MoveWasValid(new GameBoard(DefaultTestBoardSize), null));
        }

        [Theory, MemberData(nameof(InvalidMoveTestData))]
        public void ValidateMoveWithInvalidMoveReturnsFalse(IGameBoard originalBoard, IGameBoard changedBoard)
        {
            Assert.False(new GameBoardValidator(DefaultTestBoardSize).MoveWasValid(originalBoard, changedBoard));
        }

        public static IEnumerable<object[]> InvalidMoveTestData() => new[]
        {
            // Multiple moves in a row by O on empty board
            new object[]
            {
                new GameBoard(DefaultTestBoardSize),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.O, 0, 0), new BoardSpace(SpaceValue.O, 1, 1) })
            },

            // Multiple moves in a row by X on non empty board
            new object[]
            {
                new GameBoard(DefaultTestBoardSize),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.X, 0, 0), new BoardSpace(SpaceValue.X, 1, 1) })
            },

            // Multiple moves in a row by O on non empty board
            new object[]
            {
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.X, 1, 1) }),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.O, 2, 2), new BoardSpace(SpaceValue.O, 1, 2) })
            },

            // Multiple moves in a row by X on non empty board
            new object[]
            {
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.O, 1, 0) }),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.X, 1, 1), new BoardSpace(SpaceValue.X, 2, 1) })
            },

            // Two different values on same space
            new object[]
            {
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.X, 0, 0) }),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.O, 0, 0) })
            }
        };

        [Theory, MemberData(nameof(ValidMoveTestData))]
        public void ValidateMoveWithValidMoveReturnsTrue(IGameBoard originalBoard, IGameBoard changedBoard)
        {
            Assert.True(new GameBoardValidator(DefaultTestBoardSize).MoveWasValid(originalBoard, changedBoard));
        }

        public static IEnumerable<object[]> ValidMoveTestData() => new[]
        {
            // X on empty board
            new object[]
            {
                new GameBoard(DefaultTestBoardSize),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.X, 1, 1) })
            },

            // O on empty board
            new object[]
            {
                new GameBoard(DefaultTestBoardSize),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.O, 0, 0) })
            },

            // X becomes even with O
            new object[]
            {
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.O, 0, 0) }),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.O, 0, 0), new BoardSpace(SpaceValue.X, 1, 1) })
            },

            // O becomes even with X
            new object[]
            {
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.X, 1, 1) }),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.X, 1, 1), new BoardSpace(SpaceValue.O, 2, 2) })
            },

            // X pulls ahead of O
            new object[]
            {
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.X, 1, 1), new BoardSpace(SpaceValue.O, 0, 0) }),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.X, 1, 1), new BoardSpace(SpaceValue.O, 0, 0), new BoardSpace(SpaceValue.X, 2, 1) })
            },

            // O pulls ahead of X
            new object[]
            {
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.O, 2, 2), new BoardSpace(SpaceValue.X, 1, 1) }),
                CreateTestGameBoard(new List<BoardSpace> { new BoardSpace(SpaceValue.O, 2, 2), new BoardSpace(SpaceValue.X, 1, 1), new BoardSpace(SpaceValue.O, 1, 2) })
            }
        };
    }
}
