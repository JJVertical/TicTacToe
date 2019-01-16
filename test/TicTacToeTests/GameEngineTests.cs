using System;
using System.Collections.Generic;
using Xunit;

namespace TicTacToe.Tests
{
    public class GameEngineTests
    {
        [Fact]
        public void CtorPlayersSameSpaceValueShouldThrowPlayersSameSpaceValueException()
        {
            Assert.Throws<PlayersSameSpaceValueException>(() => new GameEngine(new Player("X", SpaceValue.X), new Player("O", SpaceValue.X)));
        }

        [Fact]
        public void CtorPlayersSameNameShouldThrowPlayersSameSpaceValueException()
        {
            Assert.Throws<PlayersSameNameException>(() => new GameEngine(new Player("X", SpaceValue.X), new Player("X", SpaceValue.O)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void CtorBoardSizeInvalidShouldThrowArgumentOutOfRangeException(int boardSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameEngine(new Player("X", SpaceValue.X), new Player("O", SpaceValue.O), boardSize));
        }

        [Fact]
        public void CtorPlayerGoingFirstInvalidShouldThrowInvalidPlayerGoingFirstException()
        {
            Assert.Throws<InvalidPlayerGoingFirstException>(()
                => new GameEngine(new Player("X", SpaceValue.X), new Player("O", SpaceValue.O), new Player("Y", SpaceValue.O)));
        }

        [Fact]
        public void CtorWithoutPlayerGoingFirstShouldSetCurrentPlayerToPlayer1()
        {
            var gameEngine = CreateTestGameEngine(out var player1, out var player2);
            Assert.Equal(player1, gameEngine.CurrentPlayer);
        }

        [Fact]
        public void CtorWithPlayerGoingFirstShouldSetCurrentPlayerToPlayerGoingFirst()
        {
            // arrange
            var player1 = new Player("X", SpaceValue.X);
            var player2 = new Player("O", SpaceValue.O);

            // act
            var gameEngine = new GameEngine(player1, player2, player2);

            // assert
            Assert.Equal(player2, gameEngine.CurrentPlayer);
        }

        [Fact]
        public void CtorWithValidPlayersShouldCreateNewGame()
        {
            // arrange
            var gameEngine = CreateTestGameEngine(out var player1, out var player2);

            Assert.Equal(GameState.New.ToString(), gameEngine.CurrentGameState);
            Assert.Equal(player1, gameEngine.Player1);
            Assert.Equal(player2, gameEngine.Player2);
        }

        [Theory, MemberData(nameof(CtorTestData))]
        public void CtorWithNullPlayerThrowsArgumentNullException(Player player1, Player player2)
        {
            Assert.Throws<ArgumentNullException>(() => new GameEngine(player1, player2));
        }

        public static IEnumerable<object[]> CtorTestData() => new[]
        {
            new object[] { null, new Player("X", SpaceValue.X) },
            new object[] { new Player("X", SpaceValue.X), null }
        };

        [Fact]
        public void MakeMoveOnNonAvailableSpaceShouldThrowInvalidMoveException()
        {
            // arrange
            var gameEngine = CreateTestGameEngine(out var player1, out var player2);

            // act-assert
            gameEngine.MakeMove(0, 0);
            Assert.Throws<InvalidMoveException>(() => gameEngine.MakeMove(0, 0));
        }

        [Fact]
        public void MakeMoveOnNewGameShouldMakeMove()
        {
            // arrange
            var gameEngine = CreateTestGameEngine(out var player1, out var player2);

            // act
            Assert.Raises<EventArgs>(h => gameEngine.GameStateChanged += h, h => gameEngine.GameStateChanged -= h, () => gameEngine.MakeMove(0, 0));

            // assert
            Assert.Equal(player2, gameEngine.CurrentPlayer);
            Assert.Equal(GameState.InPlay.ToString(), gameEngine.CurrentGameState);
        }

        [Fact]
        public void MakeMoveOnActiveGameShouldMakeMove()
        {
            // arrange
            var gameEngine = CreateTestGameEngine(out var player1, out var player2);

            // act
            Assert.Raises<EventArgs>(h => gameEngine.GameStateChanged += h, h => gameEngine.GameStateChanged -= h, () => gameEngine.MakeMove(0, 0));
            Assert.Raises<EventArgs>(h => gameEngine.GameStateChanged += h, h => gameEngine.GameStateChanged -= h, () => gameEngine.MakeMove(0, 1));

            // assert
            Assert.Equal(player1, gameEngine.CurrentPlayer);
            Assert.Equal(GameState.InPlay.ToString(), gameEngine.CurrentGameState);
        }

        [Fact]
        public void MakeWinningMoveShouldWinGame()
        {
            // arrange
            var gameEngine = CreateTestGameEngine(out var player1, out var player2);

            MakeActiveMove(new Coordinate(0, 0), gameEngine, player1, player2);
            MakeActiveMove(new Coordinate(0, 1), gameEngine, player2, player1);
            MakeActiveMove(new Coordinate(1, 0), gameEngine, player1, player2);
            MakeActiveMove(new Coordinate(1, 1), gameEngine, player2, player1);

            // act
            Assert.Raises<EventArgs>(h => gameEngine.GameStateChanged += h, h => gameEngine.GameStateChanged -= h, () => gameEngine.MakeMove(2, 0)); // player1 makes game winning move

            // assert
            Assert.Equal(player1, gameEngine.CurrentPlayer);
            Assert.Equal($"{GameState.WonRow}0", gameEngine.CurrentGameState);
        }

        [Fact]
        public void MakeCatsGameMoveShouldCatsGame()
        {
            // arrange
            var gameEngine = CreateTestGameEngine(out var player1, out var player2);

            MakeActiveMove(new Coordinate(0, 0), gameEngine, player1, player2);
            MakeActiveMove(new Coordinate(1, 0), gameEngine, player2, player1);
            MakeActiveMove(new Coordinate(1, 1), gameEngine, player1, player2);
            MakeActiveMove(new Coordinate(2, 0), gameEngine, player2, player1);
            MakeActiveMove(new Coordinate(2, 1), gameEngine, player1, player2);
            MakeActiveMove(new Coordinate(0, 1), gameEngine, player2, player1);
            MakeActiveMove(new Coordinate(1, 2), gameEngine, player1, player2);
            MakeActiveMove(new Coordinate(2, 2), gameEngine, player2, player1);

            // act
            Assert.Raises<EventArgs>(h => gameEngine.GameStateChanged += h, h => gameEngine.GameStateChanged -= h,  () => gameEngine.MakeMove(0, 2)); // player1 makes cats game move

            // assert
            Assert.Equal(player1, gameEngine.CurrentPlayer);
            Assert.Equal(GameState.Cats.ToString(), gameEngine.CurrentGameState);
        }

        [Fact]
        public void NewGameOnNewGameShouldCreateNewGame()
        {
            // arrange
            var gameEngine = CreateTestGameEngine(out var player1, out var player2);

            // act-assert
            gameEngine.NewGame();
        }

        [Theory, MemberData(nameof(NewGameTestData))]
        public void NewGameOnNonNewGameShouldCreateNewGame(GameEngine gameEngine)
        {
            var player = gameEngine.CurrentPlayer;
            gameEngine.NewGame();

            Assert.Equal(GameState.New.ToString(), gameEngine.CurrentGameState);
            Assert.Equal(player, gameEngine.CurrentPlayer);
        }

        public static IEnumerable<object[]> NewGameTestData() => new[]
        {
            new object[] { CreateTestGameEngine(new Coordinate[] { new Coordinate(0, 0) }) }, // first spot
            new object[] { CreateTestGameEngine(new Coordinate[] { new Coordinate(2, 2) }) }, // last spot
            new object[] { CreateTestGameEngine(new Coordinate[] { new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(0, 2) }) }, // column
            new object[] { CreateTestGameEngine(new Coordinate[] { new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0) }) }, // row
            new object[] { CreateTestGameEngine(new Coordinate[] { new Coordinate(0, 0), new Coordinate(2, 0), new Coordinate(0, 1), new Coordinate(2, 1), new Coordinate(1, 2) }) }, // random
            new object[] { CreateTestGameEngine(new Coordinate[] { new Coordinate(0, 0), new Coordinate(2, 0), new Coordinate(0, 1), new Coordinate(2, 1), new Coordinate(0, 2) }) }, // Won board

            // 1 X 1 board
            new object[]
            {
                CreateTestGameEngine(new Coordinate[]
                {
                    new Coordinate(0, 0),
                })
            },

            // 2 X 2 board
             new object[]
            {
                CreateTestGameEngine(new Coordinate[]
                {
                    new Coordinate(0, 0),
                    new Coordinate(1, 0),
                })
            },

            // full 3X3 board
            new object[]
            {
                CreateTestGameEngine(new Coordinate[]
                {
                    new Coordinate(0, 0),
                    new Coordinate(1, 0),
                    new Coordinate(1, 1),
                    new Coordinate(2, 0),
                    new Coordinate(2, 1),
                    new Coordinate(0, 1),
                    new Coordinate(1, 2),
                    new Coordinate(2, 2),
                    new Coordinate(0, 2)
                })
            },

            // full 7X7 board
            new object[]
            {
                CreateTestGameEngine(new Coordinate[]
                {
                    new Coordinate(0, 0),
                    new Coordinate(1, 0),
                    new Coordinate(2, 0),
                    new Coordinate(3, 0),
                    new Coordinate(4, 0),
                    new Coordinate(5, 0),
                    new Coordinate(6, 0),
                    new Coordinate(0, 1),
                    new Coordinate(1, 1),
                    new Coordinate(2, 1),
                    new Coordinate(3, 1),
                    new Coordinate(4, 1),
                    new Coordinate(5, 1),
                    new Coordinate(6, 1),
                    new Coordinate(0, 2),
                    new Coordinate(1, 2),
                    new Coordinate(2, 2),
                    new Coordinate(3, 2),
                    new Coordinate(4, 2),
                    new Coordinate(5, 2),
                    new Coordinate(6, 2),
                    new Coordinate(0, 3),
                    new Coordinate(1, 3),
                    new Coordinate(2, 3),
                    new Coordinate(3, 3),
                    new Coordinate(4, 3),
                    new Coordinate(5, 3),
                    new Coordinate(6, 3),
                    new Coordinate(0, 4),
                    new Coordinate(1, 4),
                    new Coordinate(2, 4),
                    new Coordinate(3, 4),
                    new Coordinate(4, 4),
                    new Coordinate(5, 4),
                    new Coordinate(6, 4),
                    new Coordinate(0, 5),
                    new Coordinate(1, 5),
                    new Coordinate(2, 5),
                    new Coordinate(3, 5),
                    new Coordinate(4, 5),
                    new Coordinate(5, 5),
                    new Coordinate(6, 5),
                    new Coordinate(1, 6),
                    new Coordinate(0, 6),
                    new Coordinate(2, 6),
                    new Coordinate(3, 6),
                    new Coordinate(4, 6),
                    new Coordinate(6, 6),
                    new Coordinate(5, 6),
                }, 7)
            }
        };

        /// <summary>
        /// Creates a new GameEngine and makes the moves for the passed in coordinates
        /// </summary>
        private static GameEngine CreateTestGameEngine(Coordinate[] coordinates, int boardSize = 3)
        {
            var gameEngine = CreateTestGameEngine(out var player1, out var player2, boardSize);

            foreach (var coordinate in coordinates)
            {
                gameEngine.MakeMove(coordinate.X, coordinate.Y);
            }

            return gameEngine;
        }

        /// <summary>
        /// Creates a GameEngine with the players passed in
        /// </summary>
        public static GameEngine CreateTestGameEngine(out Player player1, out Player player2, int boardSize = 3)
        {
            player1 = new Player("X", SpaceValue.X);
            player2 = new Player("O", SpaceValue.O);

            var gameEngine = new GameEngine(player1, player2, boardSize);

            Assert.Equal(GameState.New.ToString(), gameEngine.CurrentGameState);
            Assert.Equal(player1, gameEngine.CurrentPlayer);

            return gameEngine;
        }

        /// <summary>
        /// Makes a move on the game engine with the coordinates and asserts the players and game state
        /// </summary>
        private static void MakeActiveMove(Coordinate coordinate, GameEngine gameEngine, Player playerMakingMove, Player currentPlayerAfterMove)
        {
            Assert.Equal(playerMakingMove, gameEngine.CurrentPlayer);
            gameEngine.MakeMove(coordinate.X, coordinate.Y);
            Assert.Equal(GameState.InPlay.ToString(), gameEngine.CurrentGameState);
            Assert.Equal(currentPlayerAfterMove, gameEngine.CurrentPlayer);
        }
    }
}
