using System;
using System.Collections.Generic;

namespace TicTacToe
{
    /// <summary>
    /// Engine for keeping track of a TicTacToe game
    /// </summary>
    public class GameEngine : IGameEngine
    {
        private IGameBoard _currentGameBoard;

        /// <summary>
        /// In this constructor player1 will automatically be set to play first
        /// </summary>
        public GameEngine(Player player1, Player player2, int boardSize = Constants.DefaultBoardSize)
            : this(player1, player2, player1, boardSize) { }

        public GameEngine(Player player1, Player player2, Player playerGoingFirst, int boardSize = Constants.DefaultBoardSize)
        {
            SetCtorParameterValues(player1, player2, boardSize);
            InitializeCurrentPlayer(player1, player2, playerGoingFirst);
            NewGame();
        }

        public event EventHandler<EventArgs> GameStateChanged;

        /// <summary>
        /// Gets player1
        /// </summary>
        public Player Player1 { get; private set; }

        /// <summary>
        /// Gets player2
        /// </summary>
        public Player Player2 { get; private set; }

        /// <summary>
        /// Gets the size of the board
        /// </summary>
        public int BoardSize { get; private set; }

        /// <summary>
        /// Gets the player that plays next
        /// </summary>
        public Player CurrentPlayer { get; private set; }

        /// <summary>
        /// Gets the state of the current game board
        /// </summary>
        public string CurrentGameState { get; private set; }

        /// <summary>
        /// Validates and sets all the passed in constructor parameter values
        /// </summary>
        private void SetCtorParameterValues(Player player1, Player player2, int boardSize)
        {
            Player1 = player1 ?? throw new ArgumentNullException(nameof(player1));
            Player2 = player2 ?? throw new ArgumentNullException(nameof(player2));

            VerifyPlayersInfoNotIdentical(player1, player2);

            if (boardSize < Constants.MinimumBoardSize)
            {
                throw new ArgumentOutOfRangeException(nameof(boardSize));
            }

            BoardSize = boardSize;
        }

        /// <summary>
        /// Verifies two players information is not identical
        /// </summary>
        private void VerifyPlayersInfoNotIdentical(Player player1, Player player2)
        {
            if (player1.SpaceValue == player2.SpaceValue)
            {
                throw new PlayersSameSpaceValueException();
            }

            if (player1.Name == player2.Name)
            {
                throw new PlayersSameNameException();
            }
        }

        /// <summary>
        /// Verifies valid playerGoingFirst and then sets the CurrentPlayer to it
        /// </summary>
        private void InitializeCurrentPlayer(Player player1, Player player2, Player playerGoingFirst)
        {
            if (player1 != playerGoingFirst && player2 != playerGoingFirst)
            {
                throw new InvalidPlayerGoingFirstException();
            }

            CurrentPlayer = playerGoingFirst;
        }

        /// <summary>
        /// Clears off current game and resets game state for a new game
        /// </summary>
        public void NewGame()
        {
            _currentGameBoard = new GameBoard(BoardSize);
            CurrentGameState = GameState.New.ToString();
            OnGameStateChanged(new EventArgs());
        }

        /// <summary>
        /// Makes a move for the current player at x and y. Throws InvalidMoveException if the move was not valid.
        /// </summary>
        public void MakeMove(int x, int y)
        {
            IGameBoard newGameBoard;

            try
            {
                newGameBoard = _currentGameBoard.TakeSpace(CurrentPlayer.SpaceValue, x, y);
            }
            catch (SpaceNotAvailableException)
            {
                throw new InvalidMoveException();
            }

            if (!new GameBoardValidator(BoardSize).MoveWasValid(_currentGameBoard, newGameBoard))
            {
                throw new InvalidMoveException();
            }

            _currentGameBoard = newGameBoard;

            UpdateCurrentGameState();
        }

        /// <summary>
        /// Updates the game state of the current board
        /// </summary>
        private void UpdateCurrentGameState()
        {
            CurrentGameState = new GameStateChecker(BoardSize).CheckBoardState(_currentGameBoard);

            if (CurrentGameState.Equals(GameState.InPlay.ToString()))
            {
                SwapCurrentPlayer();
                OnGameStateChanged(new EventArgs());
                return;
            }

            if (CurrentGameState.ToString().StartsWith("Won"))
            {
                // Whoever went last remains the current player in the next game since they won
                CurrentPlayer.WonGame();
                OnGameStateChanged(new EventArgs());
                return;
            }

            if (CurrentGameState.Equals(GameState.Cats.ToString()))
            {
                // Whoever went last remains the current player for the next game since they could only create a cats game for their move
                OnGameStateChanged(new EventArgs());
                return;
            }
        }

        /// <summary>
        /// Swaps current player for non current player
        /// </summary>
        private void SwapCurrentPlayer()
        {
            if (CurrentPlayer == Player1)
            {
                CurrentPlayer = Player2;
            }
            else
            {
                CurrentPlayer = Player1;
            }
        }

        /// <summary>
        /// Returns the values of all the spaces on the board
        /// </summary>
        public IEnumerable<SpaceValue> GetBoardValues() => _currentGameBoard;

        /// <summary>
        /// Event for notifying that the current game state has changed
        /// </summary>
        public void OnGameStateChanged(EventArgs e)
        {
            GameStateChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Returns whether a space is available or not
        /// </summary>
        public bool SpaceAvailable(int x, int y) => _currentGameBoard.SpaceAvailable(x, y);
    }
}
