using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console.chess
{
    class ChessPosition
    {
        public int Line { get; set; }
        public char Column { get; set; }

        public ChessPosition(int line, char column)
        {
            Line = line;
            Column = column;
        }

        public Position ToPosition()
        {
            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString()
        {
            return $"{Column}{Line}";
        }
    }
}
