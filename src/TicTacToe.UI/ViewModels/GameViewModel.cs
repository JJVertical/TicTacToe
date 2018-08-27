using System;
using Xamarin.Forms;

namespace TicTacToe.UI.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private const string TotalWinsText = "{0} totals wins => {1}";
        private readonly IGameEngine _gameEngine;

        private string _currentGameState;
        private string _currentPlayerLabel;
        private string _player1WinsLabel;
        private string _player2WinsLabel;
        private GameSpaceViewModel[] _gameBoardArray;

        public GameViewModel(GameSettings gameSettings)
        {
            if (gameSettings == null)
            {
                throw new ArgumentNullException(nameof(gameSettings));
            }

            if (gameSettings.BoardSize < 4)
            {
                _gameEngine = new GameEngine(new Player(gameSettings.Player1Name, SpaceValue.X), new Player(gameSettings.Player2Name, SpaceValue.O), gameSettings.BoardSize);
            }
            else
            {
                _gameEngine = new GameEngine(new Player(gameSettings.Player1Name, SpaceValue.X), new Player(gameSettings.Player2Name, SpaceValue.O), gameSettings.BoardSize, true, true);
            }

            _gameEngine.GameStateChanged += GameChangedEvent;
            CurrentGameState = _gameEngine.CurrentGameState;

            InitializePlayers();
            InitializeCommands();

            UpdateGameBoard();
        }

        /// <summary>
        /// Gets or sets the current game state
        /// </summary>
        public string CurrentGameState
        {
            get { return _currentGameState; }
            set { SetProperty(ref _currentGameState, value); }
        }

        /// <summary>
        /// Gets or sets the player whos turn it is
        /// </summary>
        public string CurrentPlayerLabel
        {
            get { return _currentPlayerLabel; }
            set { SetProperty(ref _currentPlayerLabel, value); }
        }

        /// <summary>
        /// Gets or sets the text for player1's number of wins
        /// </summary>
        public string Player1WinsLabel
        {
            get { return _player1WinsLabel; }
            set { SetProperty(ref _player1WinsLabel, value); }
        }

        /// <summary>
        /// Gets or sets the text for player2's number of wins
        /// </summary>
        public string Player2WinsLabel
        {
            get { return _player2WinsLabel; }
            set { SetProperty(ref _player2WinsLabel, value); }
        }

        public Command NewGameCommand { get; private set; }

        public Command ClearInvalidMoveCommand { get; private set; }

        public Command<Button> MakeMoveCommand { get; private set; }

        /// <summary>
        /// Gets the size of the board
        /// </summary>
        public int BoardSize => _gameEngine.BoardSize;

        /// <summary>
        /// Gets or sets the Board array
        /// </summary>
        public GameSpaceViewModel[] GameBoardArray
        {
            get { return _gameBoardArray; }
            set { SetProperty(ref _gameBoardArray, value); }
        }

        /// <summary>
        /// Returns whether a board space is available
        /// </summary>
        public bool SpaceAvailable(int x, int y) => _gameEngine.SpaceAvailable(x, y);

        /// <summary>
        /// Initializes the GameViewModel command properties
        /// </summary>
        private void InitializeCommands()
        {
            NewGameCommand = new Command(NewGame);
            MakeMoveCommand = new Command<Button>(MakeMove);
        }

        /// <summary>
        /// Creates a new game
        /// </summary>
        private void NewGame()
        {
            _gameEngine.NewGame();
            UpdateCurrentPlayerLabel();
            UpdateGameBoard();
        }

        /// <summary>
        /// Initializes the GameViewModel player properties
        /// </summary>
        private void InitializePlayers()
        {
            UpdateCurrentPlayerLabel();
            UpdatePlayerWinsLabels();
        }

        /// <summary>
        /// Updates the current player's label to the player opposite of the current player
        /// </summary>
        private void UpdateCurrentPlayerLabel()
        {
            CurrentPlayerLabel = $"Current player is {_gameEngine.CurrentPlayer.Name}";
        }

        /// <summary>
        /// Updates the wins labels for each player
        /// </summary>
        private void UpdatePlayerWinsLabels()
        {
            Player1WinsLabel = string.Format(TotalWinsText, _gameEngine.Player1.Name, _gameEngine.Player1.TotalGamesWon);
            Player2WinsLabel = string.Format(TotalWinsText, _gameEngine.Player2.Name, _gameEngine.Player2.TotalGamesWon);
        }

        /// <summary>
        /// Makes a move on the board
        /// </summary>
        private void MakeMove(object sender)
        {
            var button = sender as Button;
            var x = (int)button.GetValue(Grid.ColumnProperty);
            var y = (int)button.GetValue(Grid.RowProperty);

            if (GameInPlay())
            {
                if (_gameEngine.SpaceAvailable(x, y))
                {
                    _gameEngine.MakeMove(x, y);
                    UpdateCurrentPlayerLabel();
                }
            }

            UpdateGameBoard();
        }

        /// <summary>
        /// Gets whether game is in play or not
        /// </summary>
        private bool GameInPlay() => _currentGameState.Equals(GameState.InPlay.ToString()) || _currentGameState.Equals(GameState.New.ToString());

        /// <summary>
        /// Creates a new GameBoardArray and assignes the values from the GameEngine
        /// </summary>
        private void UpdateGameBoard()
        {
            var currentIndex = 0;
            var newGameBoardArray = new GameSpaceViewModel[BoardSize * BoardSize];

            foreach (var spaceValue in _gameEngine.GetBoardValues())
            {
                newGameBoardArray[currentIndex] = new GameSpaceViewModel(spaceValue);
                currentIndex++;
            }

            GameBoardArray = newGameBoardArray;
        }

        /// <summary>
        /// Event fired when Game state is changing
        /// </summary>
        private void GameChangedEvent(object sender, EventArgs args)
        {
            CurrentGameState = _gameEngine.CurrentGameState;
            CurrentPlayerLabel = _gameEngine.CurrentPlayer.Name;

            if (CurrentGameState.ToString().StartsWith("Won"))
            {
                UpdatePlayerWinsLabels();
            }
        }

        /// <summary>
        /// Converts x and y coordinate to a space in the board array
        /// </summary>
        private int GetOffSetOfXAndY(int x, int y)
        {
            return x + (y * BoardSize);
        }
    }
}
