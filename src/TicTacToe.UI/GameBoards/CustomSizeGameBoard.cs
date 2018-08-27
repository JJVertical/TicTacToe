using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TicTacToe.UI
{
    public class CustomSizeGameBoard : Grid
    {
        private static readonly Color _backGroundColor = (Color)Application.Current.Resources["global_BackGroundColor"];
        private static readonly Color _textColor = (Color)Application.Current.Resources["global_TextColor"];
        private static readonly Color _boardGridLinesColor = (Color)Application.Current.Resources["global_GridLinesColor"];

        private static readonly double _largeFontSize = Font.SystemFontOfSize(24).FontSize;

        private static readonly Style _buttonBoardSpaceStyle = (Style)Application.Current.Resources["global_ButtonBoardSpaceStyle"];
        private static readonly Style _labelBoardSpaceStyle = (Style)Application.Current.Resources["global_LabelBoardSpaceStyle"];
        private static readonly Style _winningBoardSpaceStyle = (Style)Application.Current.Resources["global_WonBoardSpaceStyle"];

        public static readonly BindableProperty BoardSizeProperty =
            BindableProperty.Create(propertyName: nameof(BoardSize), returnType: typeof(int), declaringType: typeof(CustomSizeGameBoard), defaultValue: 0, propertyChanged: BoardSizeChanged);

        public static readonly BindableProperty MakeMoveCommandProperty =
            BindableProperty.Create(propertyName: nameof(MakeMoveCommand), returnType: typeof(Command<Button>), declaringType: typeof(CustomSizeGameBoard), defaultValue: null, propertyChanged: MakeMoveCommandChanged);

        /// <summary>
        /// Gets or sets the BoardSize bindable property value
        /// </summary>
        public int BoardSize
        {
            get { return (int)GetValue(BoardSizeProperty); }
            set { SetValue(BoardSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the MakeMoveCommand bindable property value
        /// </summary>
        public Command<Button> MakeMoveCommand
        {
            get { return (Command<Button>)GetValue(MakeMoveCommandProperty); }
            set { SetValue(MakeMoveCommandProperty, value); }
        }

        /// <summary>
        /// Delegate for the BoardSize bindable property changed
        /// </summary>
        private static void BoardSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BuildGameBoard((CustomSizeGameBoard)bindable);
        }

        /// <summary>
        /// Delegate for the MakeMoveCommand bindable property changed
        /// </summary>
        private static void MakeMoveCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BuildGameBoard((CustomSizeGameBoard)bindable);
        }

        /// <summary>
        /// Builds a game board by adding columns, rows and controls to a grid based on the BoardSize and MakeMoveCommand
        /// </summary>
        private static void BuildGameBoard(CustomSizeGameBoard gameBoardControl)
        {
            if (gameBoardControl.BoardSize > 0 && gameBoardControl.MakeMoveCommand != null)
            {
                SetGridStyle(gameBoardControl);
                AddRowDefinitions(gameBoardControl);
                AddColumnDefinitions(gameBoardControl);
                AddGridSpaceLabels(gameBoardControl);
                AddGridSpaceButtons(gameBoardControl);
            }
        }

        /// <summary>
        /// Sets the style of the game board grid
        /// </summary>
        private static void SetGridStyle(CustomSizeGameBoard gameBoardControl)
        {
            gameBoardControl.RowSpacing = 1;
            gameBoardControl.ColumnSpacing = 1;
            gameBoardControl.BackgroundColor = _boardGridLinesColor;
        }

        /// <summary>
        /// Adds the row definitions to the game board grid
        /// </summary>
        private static void AddRowDefinitions(CustomSizeGameBoard gameBoardControl)
        {
            for (var i = 0; i < gameBoardControl.BoardSize; ++i)
            {
                gameBoardControl.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
            }
        }

        /// <summary>
        /// Adds the column definitions to the game board grid
        /// </summary>
        private static void AddColumnDefinitions(CustomSizeGameBoard gameBoardControl)
        {
            for (var i = 0; i < gameBoardControl.BoardSize; ++i)
            {
                gameBoardControl.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            }
        }

        /// <summary>
        /// Adds the available space buttons to the game board grid
        /// </summary>
        private static void AddGridSpaceButtons(CustomSizeGameBoard gameBoardControl)
        {
            for (var y = 0; y < gameBoardControl.BoardSize; ++y)
            {
                for (var x = 0; x < gameBoardControl.BoardSize; ++x)
                {
                    var button = new Button
                    {
                        Command = gameBoardControl.MakeMoveCommand,
                        Style = _buttonBoardSpaceStyle,
                    };

                    var index = GetOffSetOfXAndY(x, y, gameBoardControl.BoardSize);
                    button.CommandParameter = button;
                    button.SetBinding(Button.IsVisibleProperty, new Binding($"GameBoardArray[{index}].IsAvailable"));

                    gameBoardControl.Children.Add(button, x, y);
                }
            }
        }

        /// <summary>
        /// Adds the space labels to the game board grid
        /// </summary>
        private static void AddGridSpaceLabels(CustomSizeGameBoard gameBoardControl)
        {
            WinningBoardSegments winningBoardSegments;

            if (gameBoardControl.BoardSize < 4)
            {
                winningBoardSegments = new WinningBoardSegments(gameBoardControl.BoardSize);
            }
            else
            {
                winningBoardSegments = new WinningBoardSegments(gameBoardControl.BoardSize, true, true);
            }

            for (var y = 0; y < gameBoardControl.BoardSize; ++y)
            {
                for (var x = 0; x < gameBoardControl.BoardSize; ++x)
                {
                    var label = BuildSpaceLabel(x, y, gameBoardControl.BoardSize, winningBoardSegments);
                    gameBoardControl.Children.Add(label, x, y);
                }
            }
        }

        /// <summary>
        /// Creates a label that will be used in taken spaces
        /// </summary>
        private static Label BuildSpaceLabel(int x, int y, int boardSize, WinningBoardSegments winningBoardSegments)
        {
            var index = GetOffSetOfXAndY(x, y, boardSize);
            var label = new Label();

            label.SetBinding(Label.TextProperty, $"GameBoardArray[{index}].Value");
            label.SetBinding(Label.StyleProperty, new Binding(
                                                    path: "CurrentGameState",
                                                    mode: BindingMode.OneWay,
                                                    converter: new GameStateValueConverter { BoardSpaceStyle = _labelBoardSpaceStyle, WinningBoardSpaceStyle = _winningBoardSpaceStyle },
                                                    converterParameter: BuildConverterParameter(x, y, winningBoardSegments)));

            return label;
        }

        /// <summary>
        /// Builds up a string representing the converter parameters for the taken space label
        /// </summary>
        private static string BuildConverterParameter(int x, int y, WinningBoardSegments winningBoardSegments)
        {
            var builder = new StringBuilder();

            foreach (var label in winningBoardSegments.GetWinningBoardSegmentLabels(x, y))
            {
                builder.Append($"{label}|");
            }

            // remove last |
            return builder.ToString().Substring(0, builder.Length - 1);
        }

        /// <summary>
        /// Builds a list of possible DiagonalBottom winning values
        /// </summary>
        private static IEnumerable<int> BuildWonDiagonalBottomValues(int boardSize)
        {
            var wonDiagonalBottomValues = new List<int>();

            var x = boardSize - 1;
            var y = 0;

            for (var i = 0; i < boardSize; ++i)
            {
                wonDiagonalBottomValues.Add(GetOffSetOfXAndY(x, y, boardSize));
                x--;
                y++;
            }

            return wonDiagonalBottomValues;
        }

        /// <summary>
        /// Converts x and y coordinate to a space in the board array
        /// </summary>
        private static int GetOffSetOfXAndY(int x, int y, int boardSize)
        {
            return x + (y * boardSize);
        }
    }
}
