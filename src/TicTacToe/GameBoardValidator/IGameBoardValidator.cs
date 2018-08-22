namespace TicTacToe
{
    /// <summary>
    /// Interface for GameBoard validator routines
    /// </summary>
    public interface IGameBoardValidator
    {
        /// <summary>
        /// Validates the move in the changed board against the original board to make sure it was valid
        /// </summary>
        bool MoveWasValid(IGameBoard originalBoard, IGameBoard changedBoard);
    }
}
