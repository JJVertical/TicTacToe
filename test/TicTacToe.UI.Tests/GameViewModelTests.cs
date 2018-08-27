using System;
using TicTacToe.UI.ViewModels;
using Xunit;

namespace TicTacToe.UI.Tests
{
    public class GameViewModelTests
    {
        [Fact]
        public void CtorNullGameSettingsShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GameViewModel(null));
        }
    }
}
