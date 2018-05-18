using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    /// <summary>
    /// Represents a TicTacToe gameboard
    /// </summary>
    public class GameBoard : IGameBoard
    {
        private SpaceValue[] _board;

        private GameBoard(SpaceValue[] board, int boardSize)
        {
            BoardSize = boardSize;
            _board = board;
        }

        public GameBoard(int boardSize)
        {
            if (boardSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(boardSize));
            }

            BoardSize = boardSize;

            _board = CreateEmptyBoard();
        }

        /// <summary>
        /// Gets the size of the board
        /// </summary>
        public int BoardSize { get; private set; }

        /// <summary>
        /// Creates an empty game board
        /// </summary>
        private SpaceValue[] CreateEmptyBoard()
        {
            var board = new SpaceValue[BoardSize * BoardSize];

            for (var i = 0; i < board.Length; ++i)
            {
                board[i] = SpaceValue.Available;
            }

            return board;
        }

        /// <summary>
        /// Returns whether board is full or not
        /// </summary>
        public bool BoardIsFull() => !_board.Any(s => s == SpaceValue.Available);

        /// <summary>
        /// Returns whether board is empty or not
        /// </summary>
        public bool BoardIsEmpty() => _board[0] == SpaceValue.Available && _board.Distinct().Count() == 1;

        /// <summary>
        /// Takes the board space of x and y with the value passed in and returns an new GameBoard
        /// </summary>
        public IGameBoard TakeSpace(SpaceValue spaceValue, int x, int y)
        {
            CheckXAndYInRange(x, y);

            if (!SpaceAvailable(x, y))
            {
                throw new SpaceNotAvailableException();
            }

            var newBoard = _board.ToArray();
            newBoard[GetOffSetOfXAndY(x, y)] = spaceValue;

            return new GameBoard(newBoard, BoardSize);
        }

        /// <summary>
        /// Returns whether a space is available or not
        /// </summary>
        public bool SpaceAvailable(int x, int y)
        {
            CheckXAndYInRange(x, y);

            return GetSpaceValue(x, y) == SpaceValue.Available;
        }

        /// <summary>
        /// Returns the value of the board at x and y
        /// </summary>
        public SpaceValue GetSpaceValue(int x, int y)
        {
            CheckXAndYInRange(x, y);

            return _board[GetOffSetOfXAndY(x, y)];
        }

        /// <summary>
        /// Verifies that x and y are on the board
        /// </summary>
        private void CheckXAndYInRange(int x, int y)
        {
            if (x < 0 || x >= BoardSize)
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (y < 0 || y >= BoardSize)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }
        }

        /// <summary>
        /// Converts x and y coordinate to a space in the board array
        /// </summary>
        private int GetOffSetOfXAndY(int x, int y)
        {
            return x + (y * BoardSize);
        }

        /// <summary>
        /// Returns enumerator of board spaces
        /// </summary>
        public IEnumerator<SpaceValue> GetEnumerator()
        {
            return _board.AsEnumerable().GetEnumerator();
        }

        /// <summary>
        /// Returns enumerator of board spaces
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _board.AsEnumerable().GetEnumerator();
        }
    }
}
