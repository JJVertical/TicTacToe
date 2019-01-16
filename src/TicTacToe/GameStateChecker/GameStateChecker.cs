using System;

namespace TicTacToe
{
    /// <summary>
    /// Routines to check the GameState of a board
    /// </summary>
    public class GameStateChecker : IGameStateChecker
    {
        private readonly int _boardSize;

        private readonly WinningBoardSegments _winningBoardSegments;

        public GameStateChecker(int boardSize, bool includeDiamonds = false, bool includeSquares = false)
        {
            if (boardSize < Constants.MinimumBoardSize)
            {
                throw new ArgumentOutOfRangeException(nameof(boardSize));
            }

            _boardSize = boardSize;

            _winningBoardSegments = new WinningBoardSegments(boardSize, includeDiamonds, includeSquares);
        }

        /// <summary>
        /// Returns the current game state of a GameBoard
        /// </summary>
        public string CheckBoardState(IGameBoard gameBoard)
        {
            if (gameBoard == null)
            {
                throw new ArgumentNullException(nameof(gameBoard));
            }

            if (_boardSize != gameBoard.BoardSize)
            {
                throw new ArgumentOutOfRangeException($"GameStateChecker's BoardSize({_boardSize}) does not match the GameBoard's BoardSize({gameBoard.BoardSize})");
            }

            var winningGameState = CheckWinningGameBoard(gameBoard);
            if (winningGameState != GameState.NotWinning.ToString())
            {
                return winningGameState;
            }

            if (IsCatsGame(gameBoard))
            {
                return GameState.Cats.ToString();
            }

            return GameState.InPlay.ToString();
        }

        /// <summary>
        /// Returns whether the board is a cats game or not. This routine assumes that all the segments on the board have been checked for a winning segment
        /// </summary>
        private bool IsCatsGame(IGameBoard gameBoard) => gameBoard.BoardIsFull();

        /// <summary>
        /// If winning board returns winning GameState otherwise returns GameState.NotWinning
        /// </summary>
        private string CheckWinningGameBoard(IGameBoard gameBoard)
        {
            foreach (var winningSegment in _winningBoardSegments.GetAllWinningBoardSegments())
            {
                if (SegmentWon(gameBoard, winningSegment))
                {
                    return winningSegment.Label;
                }
            }

            return GameState.NotWinning.ToString();
        }

        /// <summary>
        /// Returns whether a segment on the board is a winning segment or not
        /// </summary>
        private static bool SegmentWon(IGameBoard gameBoard, WinningSegment winningSegment)
        {
            var firstValue = gameBoard.GetSpaceValue(winningSegment.Coordinates[0].X, winningSegment.Coordinates[0].Y);

            foreach (var coordinate in winningSegment.Coordinates)
            {
                var spaceValue = gameBoard.GetSpaceValue(coordinate.X, coordinate.Y);

                if (spaceValue == SpaceValue.Available || spaceValue != firstValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
