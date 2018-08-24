using System;

namespace TicTacToe
{
    [Serializable]
    public class PlayersSameSpaceValueException : Exception
    {
        public PlayersSameSpaceValueException() { }
    }
}
