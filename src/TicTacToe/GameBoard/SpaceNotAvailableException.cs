using System;

namespace TicTacToe
{
    /// <summary>
    /// Exception when trying to take a non available GameBoard space
    /// </summary>
    [Serializable]
    public class SpaceNotAvailableException : Exception
    {
        public SpaceNotAvailableException() { }
    }
}
