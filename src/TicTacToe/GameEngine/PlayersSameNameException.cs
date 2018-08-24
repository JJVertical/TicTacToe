using System;

namespace TicTacToe
{
    [Serializable]
    public class PlayersSameNameException : Exception
    {
        public PlayersSameNameException() { }
    }
}
