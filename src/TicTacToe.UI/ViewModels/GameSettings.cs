using System;

namespace TicTacToe.UI.ViewModels
{
    public class GameSettings
    {
        public GameSettings(string player1Name, string player2Name, int boardSize)
        {
            if (string.IsNullOrEmpty(player1Name))
            {
                throw new ArgumentNullException(nameof(player1Name));
            }

            if (string.IsNullOrEmpty(player2Name))
            {
                throw new ArgumentNullException(nameof(player2Name));
            }

            if (boardSize < Constants.MinimumBoardSize)
            {
                throw new ArgumentOutOfRangeException(nameof(boardSize));
            }

            Player1Name = player1Name;
            Player2Name = player2Name;
            BoardSize = boardSize;
        }

        public string Player1Name { get; }

        public string Player2Name { get; }

        public int BoardSize { get; }
    }
}
