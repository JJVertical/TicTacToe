using System;

namespace TicTacToe
{
    [Serializable]
    public class InvalidPlayerGoingFirstException : Exception
    {
        public InvalidPlayerGoingFirstException() { }
    }
}
