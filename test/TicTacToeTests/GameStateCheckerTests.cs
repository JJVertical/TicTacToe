using System;
using System.Collections.Generic;
using Xunit;

namespace TicTacToe.Tests
{
    public partial class GameStateCheckerTests : BaseGameBoardTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void CtorInvalidBoardSizeShouldThrowArgumentOutOfRangeException(int boardSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameStateChecker(boardSize));
        }

        [Fact]
        public void CheckBoardStateNullShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GameStateChecker(3).CheckBoardState(null));
        }

        [Fact]
        public void CheckBoardStateWithDifferentBoardSizesShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameStateChecker(3).CheckBoardState(new GameBoard(7)));
        }

        [Fact]
        public void CatsGameBoardShouldReturnCatsGame()
        {
            var gameBoard = CreateTestGameBoard(new BoardSpace[]
                            {
                                new BoardSpace(SpaceValue.O, 0, 0),
                                new BoardSpace(SpaceValue.X, 1, 0),
                                new BoardSpace(SpaceValue.X, 2, 0),
                                new BoardSpace(SpaceValue.X, 0, 1),
                                new BoardSpace(SpaceValue.O, 1, 1),
                                new BoardSpace(SpaceValue.O, 2, 1),
                                new BoardSpace(SpaceValue.O, 0, 2),
                                new BoardSpace(SpaceValue.X, 1, 2),
                                new BoardSpace(SpaceValue.X, 2, 2)
                            });

            Assert.Equal(GameState.Cats.ToString(), new GameStateChecker(3).CheckBoardState(gameBoard));
        }

        [Fact]
        public void ActiveGameBoardShouldReturnActive()
        {
            var gameBoard = CreateTestGameBoard(new BoardSpace[]
                            {
                            new BoardSpace(SpaceValue.X, 0, 0),
                            new BoardSpace(SpaceValue.O, 1, 0),
                            new BoardSpace(SpaceValue.X, 2, 0),
                            new BoardSpace(SpaceValue.X, 0, 1),
                            new BoardSpace(SpaceValue.O, 1, 1),
                            new BoardSpace(SpaceValue.X, 2, 1),
                            });

            Assert.Equal(GameState.InPlay.ToString(), new GameStateChecker(3).CheckBoardState(gameBoard));
        }

        [Theory, MemberData(nameof(WonGameTestData))]
        public void GameWinningBoardShouldReturnWon(IGameBoard gameBoard, string winningGameState, int boardSize = 3)
        {
            Assert.Equal(winningGameState, new GameStateChecker(boardSize).CheckBoardState(gameBoard));
        }

        public static IEnumerable<object[]> WonGameTestData() => new[]
        {
            // diaganol left to right (boardsize 3)
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 0, 0),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 2, 2),
                    new BoardSpace(SpaceValue.O, 1, 0),
                    new BoardSpace(SpaceValue.O, 2, 0)
                }),
                GameState.WonDiagonalTop
            },

            // diaganol left to right (boardsize 7)
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 0, 0),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 2, 2),
                    new BoardSpace(SpaceValue.X, 3, 3),
                    new BoardSpace(SpaceValue.X, 4, 4),
                    new BoardSpace(SpaceValue.X, 5, 5),
                    new BoardSpace(SpaceValue.X, 6, 6),
                    new BoardSpace(SpaceValue.O, 1, 0),
                    new BoardSpace(SpaceValue.O, 2, 0),
                    new BoardSpace(SpaceValue.O, 3, 0),
                    new BoardSpace(SpaceValue.O, 4, 0),
                    new BoardSpace(SpaceValue.O, 5, 0)
                }, boardSize: 7),
                GameState.WonDiagonalTop,
                7
            },

            // diaganol right to left (boardsize 3)
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 2, 0),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 0, 2),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 1, 0)
                }, boardSize: 3),
                GameState.WonDiagonalBottom
            },

            // diaganol right to left (boardsize 7)
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 6, 0),
                    new BoardSpace(SpaceValue.X, 5, 1),
                    new BoardSpace(SpaceValue.X, 4, 2),
                    new BoardSpace(SpaceValue.X, 3, 3),
                    new BoardSpace(SpaceValue.X, 2, 4),
                    new BoardSpace(SpaceValue.X, 1, 5),
                    new BoardSpace(SpaceValue.X, 0, 6),
                    new BoardSpace(SpaceValue.O, 1, 0),
                    new BoardSpace(SpaceValue.O, 2, 0),
                    new BoardSpace(SpaceValue.O, 3, 0),
                    new BoardSpace(SpaceValue.O, 4, 0),
                    new BoardSpace(SpaceValue.O, 5, 0)
                }, boardSize: 7),
                GameState.WonDiagonalBottom,
                7
            },

            // accross top (boardsize 3)
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 0, 0),
                    new BoardSpace(SpaceValue.X, 1, 0),
                    new BoardSpace(SpaceValue.X, 2, 0),
                    new BoardSpace(SpaceValue.O, 2, 1),
                    new BoardSpace(SpaceValue.O, 1, 1)
                }, boardSize: 3),
                $"{GameState.WonRow}0"
            },

            // accross top (boardsize 7)
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 0, 0),
                    new BoardSpace(SpaceValue.X, 1, 0),
                    new BoardSpace(SpaceValue.X, 2, 0),
                    new BoardSpace(SpaceValue.X, 3, 0),
                    new BoardSpace(SpaceValue.X, 4, 0),
                    new BoardSpace(SpaceValue.X, 5, 0),
                    new BoardSpace(SpaceValue.X, 6, 0),
                    new BoardSpace(SpaceValue.O, 0, 1),
                    new BoardSpace(SpaceValue.O, 1, 1),
                    new BoardSpace(SpaceValue.O, 2, 1),
                    new BoardSpace(SpaceValue.O, 3, 1),
                    new BoardSpace(SpaceValue.O, 4, 1),
                    new BoardSpace(SpaceValue.O, 5, 1)
                }, boardSize: 7),
                $"{GameState.WonRow}0",
                7
            },

            // accross middle
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 0, 1),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 2, 1),
                    new BoardSpace(SpaceValue.O, 2, 2),
                    new BoardSpace(SpaceValue.O, 1, 0)
                }),
                $"{GameState.WonRow}1"
            },

            // accross bottom
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 0, 2),
                    new BoardSpace(SpaceValue.X, 1, 2),
                    new BoardSpace(SpaceValue.X, 2, 2),
                    new BoardSpace(SpaceValue.O, 2, 1),
                    new BoardSpace(SpaceValue.O, 1, 1)
                }),
                $"{GameState.WonRow}2"
            },

            // down left
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 0, 0),
                    new BoardSpace(SpaceValue.X, 0, 1),
                    new BoardSpace(SpaceValue.X, 0, 2),
                    new BoardSpace(SpaceValue.O, 2, 1),
                    new BoardSpace(SpaceValue.O, 1, 1)
                }),
                $"{GameState.WonColumn}0"
            },

            // down middle (boardsize 3)
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 1, 0),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 1, 2),
                    new BoardSpace(SpaceValue.O, 2, 2),
                    new BoardSpace(SpaceValue.O, 2, 1)
                }, boardSize: 3),
                $"{GameState.WonColumn}1"
            },

            // down middle (boardsize 7)
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 1, 0),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 1, 2),
                    new BoardSpace(SpaceValue.X, 1, 3),
                    new BoardSpace(SpaceValue.X, 1, 4),
                    new BoardSpace(SpaceValue.X, 1, 5),
                    new BoardSpace(SpaceValue.X, 1, 6),
                    new BoardSpace(SpaceValue.O, 2, 0),
                    new BoardSpace(SpaceValue.O, 2, 1),
                    new BoardSpace(SpaceValue.O, 2, 2),
                    new BoardSpace(SpaceValue.O, 2, 3),
                    new BoardSpace(SpaceValue.O, 2, 4),
                    new BoardSpace(SpaceValue.O, 2, 5)
                }, boardSize: 7),
                $"{GameState.WonColumn}1",
                7
            },

            // down right
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 2, 0),
                    new BoardSpace(SpaceValue.X, 2, 1),
                    new BoardSpace(SpaceValue.X, 2, 2),
                    new BoardSpace(SpaceValue.O, 1, 1),
                    new BoardSpace(SpaceValue.O, 1, 0)
                }),
                $"{GameState.WonColumn}2"
            },

            // full winning board
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 0, 0),
                    new BoardSpace(SpaceValue.O, 1, 0),
                    new BoardSpace(SpaceValue.X, 2, 0),
                    new BoardSpace(SpaceValue.O, 0, 1),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.O, 2, 1),
                    new BoardSpace(SpaceValue.X, 0, 2),
                    new BoardSpace(SpaceValue.O, 1, 2),
                    new BoardSpace(SpaceValue.O, 2, 2)
                }),
                GameState.WonDiagonalBottom.ToString()
            },

            // two winers
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 0, 0),
                    new BoardSpace(SpaceValue.O, 1, 0),
                    new BoardSpace(SpaceValue.O, 2, 0),
                    new BoardSpace(SpaceValue.X, 0, 1),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.O, 2, 1),
                    new BoardSpace(SpaceValue.X, 0, 2),
                    new BoardSpace(SpaceValue.O, 1, 2),
                    new BoardSpace(SpaceValue.O, 2, 2)
                }),
                $"{GameState.WonColumn}0"
            }
        };

        [Theory, MemberData(nameof(WonSquareGameTestData))]
        public void SquareGameWinningBoardShouldReturnWonSquare(IGameBoard gameBoard, int col, int row, int boardSize = 3)
        {
            Assert.Equal($"{GameState.WonSquare.ToString()}{col}{row}", new GameStateChecker(boardSize: boardSize, includeSquares: true).CheckBoardState(gameBoard));
        }

        public static IEnumerable<object[]> WonSquareGameTestData() => new[]
        {
            // square 1st column 1st row 3X3
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 0, 0),
                    new BoardSpace(SpaceValue.X, 0, 1),
                    new BoardSpace(SpaceValue.X, 1, 0),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.O, 2, 0),
                    new BoardSpace(SpaceValue.O, 2, 1),
                    new BoardSpace(SpaceValue.O, 1, 2)
                }), 0, 0
            },

            // square 1st col 2nd row 3X3
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                new BoardSpace(SpaceValue.X, 0, 1),
                new BoardSpace(SpaceValue.X, 0, 2),
                new BoardSpace(SpaceValue.X, 1, 1),
                new BoardSpace(SpaceValue.X, 1, 2),
                new BoardSpace(SpaceValue.O, 0, 0),
                new BoardSpace(SpaceValue.O, 1, 0),
                new BoardSpace(SpaceValue.O, 2, 1)
                }), 0, 1
            },

            // square 2nd column 1st row 3X3
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 1, 0),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 2, 0),
                    new BoardSpace(SpaceValue.X, 2, 1),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 1),
                    new BoardSpace(SpaceValue.O, 1, 2)
                }), 1, 0
            },

            // square 2nd col 2nd row 3X3
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                new BoardSpace(SpaceValue.X, 1, 1),
                new BoardSpace(SpaceValue.X, 1, 2),
                new BoardSpace(SpaceValue.X, 2, 1),
                new BoardSpace(SpaceValue.X, 2, 2),
                new BoardSpace(SpaceValue.O, 0, 0),
                new BoardSpace(SpaceValue.O, 1, 0),
                new BoardSpace(SpaceValue.O, 0, 1)
                }), 1, 1
            },

            // square 2nd col 1st row 4X4
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 1, 0),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 2, 0),
                    new BoardSpace(SpaceValue.X, 2, 1),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 1),
                    new BoardSpace(SpaceValue.O, 1, 2)
                }, boardSize: 4), 1, 0, 4
            },

            // square 2nd col 2nd row 4X4
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 1, 2),
                    new BoardSpace(SpaceValue.X, 2, 1),
                    new BoardSpace(SpaceValue.X, 2, 2),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 1),
                    new BoardSpace(SpaceValue.O, 1, 3)
                }, boardSize: 4), 1, 1, 4
            },

            // square 2nd col 3rd row 4X4
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 1, 2),
                    new BoardSpace(SpaceValue.X, 1, 3),
                    new BoardSpace(SpaceValue.X, 2, 2),
                    new BoardSpace(SpaceValue.X, 2, 3),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 1),
                    new BoardSpace(SpaceValue.O, 1, 0)
                }, boardSize: 4), 1, 2, 4
            },

            // square 3rd col 1st row 4X4
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 2, 0),
                    new BoardSpace(SpaceValue.X, 2, 1),
                    new BoardSpace(SpaceValue.X, 3, 0),
                    new BoardSpace(SpaceValue.X, 3, 1),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 1),
                    new BoardSpace(SpaceValue.O, 1, 2)
                }, boardSize: 4), 2, 0, 4
            },

            // square 3rd col 2nd row 4X4
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 2, 1),
                    new BoardSpace(SpaceValue.X, 2, 2),
                    new BoardSpace(SpaceValue.X, 3, 1),
                    new BoardSpace(SpaceValue.X, 3, 2),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 1),
                    new BoardSpace(SpaceValue.O, 1, 3)
                }, boardSize: 4), 2, 1, 4
            },

            // square 3rd col 3rd row 4X4
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 2, 2),
                    new BoardSpace(SpaceValue.X, 2, 3),
                    new BoardSpace(SpaceValue.X, 3, 2),
                    new BoardSpace(SpaceValue.X, 3, 3),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 1),
                    new BoardSpace(SpaceValue.O, 1, 3)
                }, boardSize: 4), 2, 2, 4
            },
        };

        [Theory, MemberData(nameof(WonDiamondGameTestData))]
        public void DiamondGameWinningBoardShouldReturnWonDiamond(IGameBoard gameBoard, int col, int row, int boardSize = 3)
        {
            Assert.Equal($"{GameState.WonDiamond.ToString()}{col}{row}", new GameStateChecker(boardSize: boardSize, includeDiamonds: true).CheckBoardState(gameBoard));
        }

        public static IEnumerable<object[]> WonDiamondGameTestData() => new[]
        {
            // diamond middle column 3X3
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 1, 0),
                    new BoardSpace(SpaceValue.X, 0, 1),
                    new BoardSpace(SpaceValue.X, 2, 1),
                    new BoardSpace(SpaceValue.X, 1, 2),
                    new BoardSpace(SpaceValue.O, 2, 2),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 2)
                }), 1, 0
            },

            // diamond 2nd col 1st row 4X4
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 1, 0),
                    new BoardSpace(SpaceValue.X, 0, 1),
                    new BoardSpace(SpaceValue.X, 2, 1),
                    new BoardSpace(SpaceValue.X, 1, 2),
                    new BoardSpace(SpaceValue.O, 1, 1),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 2)
                }, boardSize: 4), 1, 0, 4
            },

            // diamond 3rd col 1st row 4X4
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 2, 0),
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 3, 1),
                    new BoardSpace(SpaceValue.X, 2, 2),
                    new BoardSpace(SpaceValue.O, 1, 2),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 2)
                }, boardSize: 4), 2, 0, 4
            },

            // diamond 2nd col 2nd row 4X4
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 1, 1),
                    new BoardSpace(SpaceValue.X, 0, 2),
                    new BoardSpace(SpaceValue.X, 2, 2),
                    new BoardSpace(SpaceValue.X, 1, 3),
                    new BoardSpace(SpaceValue.O, 1, 0),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 3)
                }, boardSize: 4), 1, 1, 4
            },

            // diamond 3rd col 2nd row 4X4
            new object[]
            {
                CreateTestGameBoard(new BoardSpace[]
                {
                    new BoardSpace(SpaceValue.X, 2, 1),
                    new BoardSpace(SpaceValue.X, 1, 2),
                    new BoardSpace(SpaceValue.X, 3, 2),
                    new BoardSpace(SpaceValue.X, 2, 3),
                    new BoardSpace(SpaceValue.O, 2, 2),
                    new BoardSpace(SpaceValue.O, 0, 0),
                    new BoardSpace(SpaceValue.O, 0, 2)
                }, boardSize: 4), 2, 1, 4
            },
        };
    }
}
