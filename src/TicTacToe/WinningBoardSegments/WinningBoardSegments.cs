using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class WinningBoardSegments
    {
        private const int SquareSize = 4;

        private readonly int _boardSize;

        private List<WinningSegment> _winningBoardSegments = new List<WinningSegment>();

        public WinningBoardSegments(int boardSize, bool includeDiamonds = false, bool includeSquares = false)
        {
            if (boardSize < Constants.MinimumBoardSize)
            {
                throw new ArgumentOutOfRangeException(nameof(boardSize));
            }

            _boardSize = boardSize;

            InitializeWinningBoardSegments(includeDiamonds, includeSquares);
        }

        public IEnumerable<string> GetWinningBoardSegmentLabels(int col, int row)
            => _winningBoardSegments.Where(s => s.Coordinates.Any(c => c.X.Equals(col) && c.Y.Equals(row))).Select(l => l.Label);

        public IReadOnlyCollection<WinningSegment> GetAllWinningBoardSegments() => _winningBoardSegments;

        /// <summary>
        /// Adds all the possible winning segments into the list of winning board segments
        /// </summary>
        private void InitializeWinningBoardSegments(bool includeDiamonds, bool includeSquares)
        {
            AddColumnWinningBoardSegments();
            AddRowWinningBoardSegments();
            AddWonDiagonalTopWinningBoardSegments();
            AddWonDiagonalBottomWinningBoardSegments();

            if (includeDiamonds)
            {
                AddDiamondWinningBoardSegments();
            }

            if (includeSquares)
            {
                AddSquaresWinningBoardSegments();
            }
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
        /// Adds all the possible winning diamond segments inot the list of winning board segments
        /// </summary>
        private void AddDiamondWinningBoardSegments()
        {
            for (var col = 1; col < _boardSize - 1; ++col)
            {
                for (var row = 0; row < _boardSize - 2; ++row)
                {
                    {
                        AddDiamondToWinningBoardSegments(col, row);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a diamond to the winning board segment based on the row and column passed in
        /// </summary>
        private void AddDiamondToWinningBoardSegments(int col, int row)
        {
            var coordinates = new Coordinate[SquareSize];

            coordinates[0] = new Coordinate(col, row);
            coordinates[1] = new Coordinate(col - 1, row + 1);
            coordinates[2] = new Coordinate(col + 1, row + 1);
            coordinates[3] = new Coordinate(col, row + 2);

            _winningBoardSegments.Add(new WinningSegment($"{GameState.WonDiamond.ToString()}{col}{row}", coordinates));
        }

        /// <summary>
        /// Adds all the possible winning square segments inot the list of winning board segments
        /// </summary>
        private void AddSquaresWinningBoardSegments()
        {
            for (var col = 0; col < _boardSize - 1; ++col)
            {
                for (var row = 0; row < _boardSize - 1; ++row)
                {
                    AddSquareToWinningBoardSegments(col, row);
                }
            }
        }

        /// <summary>
        /// Adds a square to the winning board segment based on the row and column passed in
        /// </summary>
        private void AddSquareToWinningBoardSegments(int col, int row)
        {
            var coordinates = new Coordinate[SquareSize];

            coordinates[0] = new Coordinate(col, row);
            coordinates[1] = new Coordinate(col + 1, row);
            coordinates[2] = new Coordinate(col, row + 1);
            coordinates[3] = new Coordinate(col + 1, row + 1);

            _winningBoardSegments.Add(new WinningSegment($"{GameState.WonSquare.ToString()}{col}{row}", coordinates));
        }
    }
}
