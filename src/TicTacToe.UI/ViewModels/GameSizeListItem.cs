using System;

namespace TicTacToe.UI.ViewModels
{
    public class GameSizeListItem
    {
        public GameSizeListItem(string displayText, int value)
        {
            if (string.IsNullOrEmpty(displayText))
            {
                throw new ArgumentNullException(nameof(displayText));
            }

            if (value < Constants.MinimumBoardSize)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            DisplayText = displayText;
            Value = value;
        }

        public string DisplayText { get; }

        public int Value { get; }
    }
}
