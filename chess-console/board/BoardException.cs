using System;
using System.Collections.Generic;
using System.Text;

namespace chess_console.board
{
    class BoardException : Exception
    {
        public BoardException(string message) : base(message)
        {

        }
    }
}
