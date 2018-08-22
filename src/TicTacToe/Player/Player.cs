using System;

namespace TicTacToe
{
    /// <summary>
    /// Represents a game player
    /// </summary>
    public class Player
    {
        public Player(string name, SpaceValue spaceValue)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(Name));
            }

            if (spaceValue == SpaceValue.Available)
            {
                throw new PlayersSpaceValueAvailableException();
            }

            Name = name;
            SpaceValue = spaceValue;
        }

        /// <summary>
        /// Gets name of player
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets value that represents the player on the game board
        /// </summary>
        public SpaceValue SpaceValue { get; }

        /// <summary>
        /// Gets the total number of games the player has won
        /// </summary>
        public int TotalGamesWon { get; private set; }

        /// <summary>
        /// Method for incrementing the total number of games won
        /// </summary>
        public void WonGame()
        {
            TotalGamesWon++;
        }

        /// <summary>
        /// Resets the total number of games won to zero
        /// </summary>
        public void ClearTotalGamesWon()
        {
            TotalGamesWon = 0;
        }
    }
}
