using System;
using System.Collections.Generic;

namespace TicTacToe
{
    /// <summary>
    /// Interface for a game engine for a TicTacToe game
    /// </summary>
    public interface IGameEngine
    {
        event EventHandler<EventArgs> GameStateChanged;

        /// <summary>
        /// Gets player1
        /// </summary>
        Player Player1 { get; }

        /// <summary>
        /// Gets player2
        /// </summary>
        Player Player2 { get; }

        /// <summary>
        /// Gets the player that plays next
        /// </summary>
        Player CurrentPlayer { get; }

        /// <summary>
        /// Gets the state of the current game board
        /// </summary>
        string CurrentGameState { get; }

        /// <summary>
        /// Gets the size of the board
        /// </summary>
        int BoardSize { get; }

        /// <summary>
        /// Clears off current game and resets game state for a new game
        /// </summary>
        void NewGame();

        /// <summary>
        /// Makes a move for the current player at x and y. Throws InvalidMoveException if the move was not valid.
        /// </summary>
        void MakeMove(int x, int y);

        /// <summary>
        /// Returns the values of all the spaces on the board
        /// </summary>
        IEnumerable<SpaceValue> GetBoardValues();

        /// <summary>
        /// Returns whether a space is available or not
        /// </summary>
        bool SpaceAvailable(int x, int y);

        /// <summary>
        /// Event for notifying that the current game state has changed
        /// </summary>
        void OnGameStateChanged(EventArgs e);
    }
}
