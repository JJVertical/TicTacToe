using System;

namespace TicTacToe
{
    /// <summary>
    /// Class for holding a winning segment that includes coordinates and a label
    /// </summary>
    public class WinningSegment
    {
        public WinningSegment(string label, Coordinate[] coordinates)
        {
            Label = label;
            Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
        }

        public Coordinate[] Coordinates { get; }

        public string Label { get; }
    }
}
