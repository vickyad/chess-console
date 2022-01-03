using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console.chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PosibleMovements()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
