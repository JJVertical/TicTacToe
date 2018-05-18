namespace TicTacToe
{
    /// <summary>
    /// Represents the space value, x and y coordinates of a space on the GameBoard
    /// </summary>
    public class BoardSpace
    {
        public BoardSpace(SpaceValue spaceValue, int x, int y)
        {
            ValueOfSpace = spaceValue;
            X = x;
            Y = y;
        }

        public SpaceValue ValueOfSpace { get; }

        public int X { get; }

        public int Y { get; }
    }
}
