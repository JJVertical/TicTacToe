using System.Collections.Generic;

namespace TicTacToe.Tests
{
    /// <summary>
    /// Base helper routines for GameBoard testing
    /// </summary>
    public abstract class BaseGameBoardTests
    {
        protected const int DefaultTestBoardSize = 3;

        /// <summary>
        /// Creates a test board with the list of spaces and ensures the ToClear coordinates space is available
        /// </summary>
        protected static IGameBoard CreateTestGameBoard(IEnumerable<BoardSpace> boardSpaces, int xToClear, int yToClear, int boardSize = 3)
        {
            IGameBoard gameBoard = new GameBoard(boardSize);

            foreach (var boardSpace in boardSpaces)
            {
                if (boardSpace.X == xToClear && boardSpace.Y == yToClear)
                {
                    continue;
                }

                gameBoard = gameBoard.TakeSpace(boardSpace.ValueOfSpace, boardSpace.X, boardSpace.Y);
            }

            return gameBoard;
        }

        /// <summary>
        /// Creates a test game board based on passed in TestSpaceStates
        /// </summary>
        protected static IGameBoard CreateTestGameBoard(IEnumerable<BoardSpace> testSpaceStates, int boardSize = 3)
        {
            IGameBoard gameBoard = new GameBoard(boardSize);

            foreach (var testSpaceState in testSpaceStates)
            {
                gameBoard = gameBoard.TakeSpace(testSpaceState.ValueOfSpace, testSpaceState.X, testSpaceState.Y);
            }

            return gameBoard;
        }
    }
}
