namespace TicTacToe
{
    /// <summary>
    /// Interface for the set of routines for checking the GameState of a board
    /// </summary>
    public interface IGameStateChecker
    {
        /// <summary>
        /// Returns the current game state of a GameBoard
        /// </summary>
        string CheckBoardState(IGameBoard gameBoard);
    }
}
