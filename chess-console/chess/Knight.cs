using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console.chess
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PosibleMovements()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
