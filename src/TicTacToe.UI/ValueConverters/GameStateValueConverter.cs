using System;
using System.Globalization;
using Xamarin.Forms;

namespace TicTacToe.UI
{
    public class GameStateValueConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets the style to be applied to winning board space
        /// </summary>
        public Style WinningBoardSpaceStyle { get; set; }

        /// <summary>
        /// Gets or sets the style to be applied to a board space
        /// </summary>
        public Style BoardSpaceStyle { get; set; }

        /// <summary>
        /// Converts the parameter and value parameters into a space style to be returned to the caller
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EnsureSpaceStylesNotNull();

            if (value == null || parameter == null)
            {
                return BoardSpaceStyle;
            }

            return GetStyleFromGameState(value, parameter);
        }

        /// <summary>
        /// Returns WinningBoardSpaceStyle if the game space is a winning space
        /// </summary>
        private object GetStyleFromGameState(object value, object parameter)
        {
            var winningGameStates = ((string)parameter).Split('|');

            foreach (var winningGameState in winningGameStates)
            {
                if (((string)value).Equals(winningGameState, StringComparison.Ordinal))
                {
                    return WinningBoardSpaceStyle;
                }
            }

            return BoardSpaceStyle;
        }

        /// <summary>
        /// Ensures that the winning and board styles are not null
        /// </summary>
        private void EnsureSpaceStylesNotNull()
        {
            if (BoardSpaceStyle == null)
            {
                throw new NullReferenceException(nameof(BoardSpaceStyle));
            }

            if (WinningBoardSpaceStyle == null)
            {
                throw new NullReferenceException(nameof(WinningBoardSpaceStyle));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
