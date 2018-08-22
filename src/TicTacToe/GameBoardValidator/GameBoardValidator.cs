using System;
using System.Linq;

namespace TicTacToe
{
    /// <summary>
    /// GameBoard validation routines
    /// </summary>
    public class GameBoardValidator : IGameBoardValidator
    {
        private readonly int _boardSize;

        public GameBoardValidator(int boardSize)
        {
            if (boardSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(boardSize));
            }

            _boardSize = boardSize;
        }

        /// <summary>
        /// Validates the move in the changed board against the original board to make sure it was valid
        /// </summary>
        public bool MoveWasValid(IGameBoard originalBoard, IGameBoard changedBoard)
        {
            if (originalBoard == null)
            {
                throw new ArgumentNullException(nameof(originalBoard));
            }

            if (changedBoard == null)
            {
                throw new ArgumentNullException(nameof(changedBoard));
            }

            return TooManySpacesTakenByOnePlayer(changedBoard) ? false : TakenSpaceReplaced(originalBoard, changedBoard) ? false : true;
        }

        /// <summary>
        /// Returns whether an already taken space was taken by a different player
        /// </summary>
        private bool TakenSpaceReplaced(IGameBoard originalBoard, IGameBoard changedBoard)
        {
            for (var x = 0; x < _boardSize; ++x)
            {
                for (var y = 0; y < _boardSize; ++y)
                {
                    if (originalBoard.GetSpaceValue(x, y) != changedBoard.GetSpaceValue(x, y) && originalBoard.GetSpaceValue(x, y) != SpaceValue.Available)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Returns whether too many spaces were taken by one player
        /// </summary>
        private static bool TooManySpacesTakenByOnePlayer(IGameBoard changedBoard) => Math.Abs(changedBoard.Where(s => s == SpaceValue.O).Count() - changedBoard.Where(s => s == SpaceValue.X).Count()) > 1;
    }
}
