using System;

namespace TicTacToe
{
    /// <summary>
    /// Exception for a players space value being the available enum value
    /// </summary>
    [Serializable]
    public class PlayersSpaceValueAvailableException : Exception
    {
        public PlayersSpaceValueAvailableException() { }
    }
}
