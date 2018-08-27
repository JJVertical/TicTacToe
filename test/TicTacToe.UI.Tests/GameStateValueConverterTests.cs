using System;
using System.Globalization;
using Xamarin.Forms;
using Xunit;

namespace TicTacToe.UI.Tests
{
    public class GameStateValueConverterTests
    {
        private readonly Style _winningStyle = new Style(typeof(Style)) { Setters = { new Setter { Property = Label.BackgroundColorProperty, Value = Color.Red } } };
        private readonly Style _boardStyle = new Style(typeof(Style)) { Setters = { new Setter { Property = Label.BackgroundColorProperty, Value = Color.Black } } };
        private readonly CultureInfo _cultureInfo = new CultureInfo("en-US");

        [Fact]
        public void ConvertWithNullWonBoardSpaceStyleShouldThrowNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => new GameStateValueConverter() { WinningBoardSpaceStyle = null, BoardSpaceStyle = _boardStyle }.Convert("Won", typeof(Style), "Won", _cultureInfo));
        }

        [Fact]
        public void ConvertWithNullBoardSpaceStyleShouldThrowNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => new GameStateValueConverter() { WinningBoardSpaceStyle = _winningStyle, BoardSpaceStyle = null }.Convert("Won", typeof(Style), "Won", _cultureInfo));
        }

        [Fact]
        public void ConvertNullValueShouldReturnBoardStyle()
        {
            Assert.Equal(_boardStyle, CreateTestGameStateValueConverter().Convert(null, typeof(Style), new object(), _cultureInfo));
        }

        [Fact]
        public void ConvertNullParameterShouldReturnBaordSpaceStyle()
        {
            Assert.Equal(_boardStyle, CreateTestGameStateValueConverter().Convert(new object(), typeof(Style), null, _cultureInfo));
        }

        [Fact]
        public void ConvertWithWinningStateShouldReturnWinningStyle()
        {
            Assert.Equal(_winningStyle, CreateTestGameStateValueConverter().Convert("Won", typeof(Style), "Won", _cultureInfo));
        }

        [Fact]
        public void ConvertWithWinningStateAndMultiplePossibleWinningStatesShouldReturnWinningStyle()
        {
            Assert.Equal(_winningStyle, CreateTestGameStateValueConverter().Convert("WonTop", typeof(Style), "WonTop|WonBottom", _cultureInfo));
        }

        [Fact]
        public void ConvertWithNonWinningStateShouldReturnBoardStyle()
        {
            Assert.Equal(_boardStyle, CreateTestGameStateValueConverter().Convert("NotWon", typeof(Style), "Won", _cultureInfo));
        }

        [Fact]
        public void ConvertWithNonWinningStateAndMultiplePossibleWinningStatesShouldReturnBoardStyle()
        {
            Assert.Equal(_boardStyle, CreateTestGameStateValueConverter().Convert("NotWon", typeof(Style), "WonTop|WonBottom", _cultureInfo));
        }

        private GameStateValueConverter CreateTestGameStateValueConverter() => new GameStateValueConverter() { WinningBoardSpaceStyle = _winningStyle, BoardSpaceStyle = _boardStyle };
    }
}
