using System.Collections.Generic;

namespace TicTacToe
{
    /// <summary>
    /// Represents an interface for a TicTacToe gameboard
    /// </summary>
    public interface IGameBoard : IEnumerable<SpaceValue>
    {
        /// <summary>
        /// Gets the size of the board
        /// </summary>
        int BoardSize { get; }

        /// <summary>
        /// Returns whether board is full or not
        /// </summary>
        bool BoardIsFull();

        /// <summary>
        /// Returns whether board is empty or not
        /// </summary>
        bool BoardIsEmpty();

        /// <summary>
        /// Takes the board space of x and y with the value passed in and returns an new GameBoard
        /// </summary>
        IGameBoard TakeSpace(SpaceValue spaceValue, int x, int y);

        /// <summary>
        /// Returns whether space at x and y is available
        /// </summary>
        bool SpaceAvailable(int x, int y);

        /// <summary>
        /// Returns the value at x and y
        /// </summary>
        SpaceValue GetSpaceValue(int x, int y);
    }
}
