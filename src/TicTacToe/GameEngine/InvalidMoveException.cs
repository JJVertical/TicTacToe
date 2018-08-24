using System;

namespace TicTacToe
{
    [Serializable]
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException() { }
    }
}
