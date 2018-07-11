using System;
using System.Collections.Generic;

namespace TicTacToe
{
    /// <summary>
    /// Routines to check the GameState of a board
    /// </summary>
    public class GameStateChecker : IGameStateChecker
    {
        private readonly int _boardSize;

        private List<WinningSegment> _winningBoardSegments = new List<WinningSegment>();

        public GameStateChecker(int boardSize)
        {
            _boardSize = boardSize;

            InitializeWinningBoardSegments();
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
        /// Adds all the possible winning segments into the list of winning board segments
        /// </summary>
        private void InitializeWinningBoardSegments()
        {
            AddColumnWinningBoardSegments();
            AddRowWinningBoardSegments();
            AddWonDiagonalTopWinningBoardSegments();
            AddWonDiagonalBottomWinningBoardSegments();
        }

        /// <summary>
        /// Adds all the possible winning row segments into the list of winning board segments
        /// </summary>
        private void AddRowWinningBoardSegments()
        {
            for (var row = 0; row < _boardSize; ++row)
            {
                var coordinates = new Coordinate[_boardSize];

                for (var col = 0; col < _boardSize; ++col)
                {
                    coordinates[col] = new Coordinate(col, row);
                }

                _winningBoardSegments.Add(new WinningSegment($"{GameState.WonRow}{row}", coordinates));
            }
        }

        /// <summary>
        /// Adds all the possible winning column segments into the list of winning board segments
        /// </summary>
        private void AddColumnWinningBoardSegments()
        {
            for (var col = 0; col < _boardSize; ++col)
            {
                var coordinates = new Coordinate[_boardSize];

                for (var row = 0; row < _boardSize; ++row)
                {
                    coordinates[row] = new Coordinate(col, row);
                }

                _winningBoardSegments.Add(new WinningSegment($"{GameState.WonColumn}{col}", coordinates));
            }
        }

        /// <summary>
        /// Adds all the possible winning diagonal top left to bottom right segments into the list of winning board segments
        /// </summary>
        private void AddWonDiagonalTopWinningBoardSegments()
        {
            var coordinates = new Coordinate[_boardSize];

            for (var i = 0; i < _boardSize; ++i)
            {
                coordinates[i] = new Coordinate(i, i);
            }

            _winningBoardSegments.Add(new WinningSegment(GameState.WonDiagonalTop.ToString(), coordinates));
        }

        /// <summary>
        /// Adds all the possible winning diagonal bottom left to top right segments into the list of winning board segments
        /// </summary>
        private void AddWonDiagonalBottomWinningBoardSegments()
        {
            var row = 0;
            var col = _boardSize - 1;
            var coordinates = new Coordinate[_boardSize];

            for (var i = 0; i < _boardSize; ++i)
            {
                coordinates[i] = new Coordinate(col, row);
                row++;
                col--;
            }

            _winningBoardSegments.Add(new WinningSegment(GameState.WonDiagonalBottom.ToString(), coordinates));
        }

        /// <summary>
        /// If winning board returns winning GameState otherwise returns GameState.NotWinning
        /// </summary>
        private string CheckWinningGameBoard(IGameBoard gameBoard)
        {
            foreach (var winningSegment in _winningBoardSegments)
            {
                if (SegmentWon(gameBoard, winningSegment))
                {
                    return winningSegment.WinningGameState;
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

        /// <summary>
        /// Class for holding a winning segment that includes coordinates and a GameState
        /// </summary>
        private class WinningSegment
        {
            public WinningSegment(string winningGameState, Coordinate[] coordinates)
            {
                WinningGameState = winningGameState;
                Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
            }

            public Coordinate[] Coordinates { get; }

            public string WinningGameState { get; }
        }
    }
}
