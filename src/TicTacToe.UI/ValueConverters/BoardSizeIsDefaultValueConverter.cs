using System;
using System.Globalization;
using Xamarin.Forms;

namespace TicTacToe.UI
{
    public class BoardSizeIsDefaultValueConverter : IValueConverter
    {
        /// <summary>
        /// Returns whether the boardsize=default board size matches the boolean parameter passed in
        /// </summary>
        /// <param name="value">Size of the board</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Boolean for what boardsize==defaultboardsize should be</param>
        /// <param name="culture"></param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return true;
            }

            return ((int)value == Constants.DefaultBoardSize) == bool.Parse((string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
