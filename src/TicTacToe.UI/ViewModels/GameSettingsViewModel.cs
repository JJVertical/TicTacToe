namespace TicTacToe.UI.ViewModels
{
    public class GameSettingsViewModel : BaseViewModel
    {
        private GameSizeListItem _currentlySelectedGameSize;
        private string _player1Name = GameUIConstants.DefaultPlayer1Name;
        private string _player2Name = GameUIConstants.DefaultPlayer2Name;

        public GameSettingsViewModel()
        {
            BuildListOfAvailableGameBoardSizes();
            CurrentlySelectedGameSize = AvailableGameSizes[0];
        }

        public GameSizeListItem[] AvailableGameSizes { get; private set; }

        public GameSizeListItem CurrentlySelectedGameSize
        {
            get { return _currentlySelectedGameSize; }
            set { SetProperty(ref _currentlySelectedGameSize, value); }
        }

        public string Player1Name
        {
            get { return _player1Name; }
            set { SetProperty(ref _player1Name, value); }
        }

        public string Player2Name
        {
            get { return _player2Name; }
            set { SetProperty(ref _player2Name, value); }
        }

        /// <summary>
        /// Builds the list of available game board sizes for the game
        /// </summary>
        private void BuildListOfAvailableGameBoardSizes()
        {
            AvailableGameSizes = new GameSizeListItem[] { new GameSizeListItem("3 X 3", 3), new GameSizeListItem("4 X 4", 4) };
        }
    }
}
