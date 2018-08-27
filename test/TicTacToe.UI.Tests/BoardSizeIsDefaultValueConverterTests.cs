using System.Globalization;
using Xunit;

namespace TicTacToe.UI.Tests
{
    public class BoardSizeIsDefaultValueConverterTests
    {
        private readonly CultureInfo _cultureInfo = new CultureInfo("en-US");

        [Fact]
        public void ConvertNullValueShouldReturnTrue()
        {
            Assert.True((bool)new BoardSizeIsDefaultValueConverter().Convert(null, typeof(bool), "true", _cultureInfo));
        }

        [Fact]
        public void ConvertNullParameterShouldReturnTrue()
        {
            Assert.True((bool)new BoardSizeIsDefaultValueConverter().Convert(Constants.DefaultBoardSize, typeof(bool), null, _cultureInfo));
        }

        [Fact]
        public void ConvertWithDefaultBoardSizeAndTrueParameterShouldReturnTrue()
        {
            Assert.True((bool)new BoardSizeIsDefaultValueConverter().Convert(Constants.DefaultBoardSize, typeof(bool), "true", _cultureInfo));
        }

        [Fact]
        public void ConvertWithDefaultBoardSizeAndFalseParameterShouldReturnFalse()
        {
            Assert.False((bool)new BoardSizeIsDefaultValueConverter().Convert(4, typeof(bool), "true", _cultureInfo));
        }

        [Fact]
        public void ConvertWithNonDefaultBoardSizeAndTrueParameterShouldReturnFalse()
        {
            Assert.False((bool)new BoardSizeIsDefaultValueConverter().Convert(4, typeof(bool), "true", _cultureInfo));
        }

        [Fact]
        public void ConvertWithNonDefaultBoardSizeAndFalseParameterShouldReturnTrue()
        {
            Assert.True((bool)new BoardSizeIsDefaultValueConverter().Convert(Constants.DefaultBoardSize, typeof(bool), "true", _cultureInfo));
        }
    }
}
