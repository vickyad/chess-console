using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console.chess
{
    class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PosibleMovements()
        {
            bool[,] posibleMovements = new bool[Board.Lines, Board.Columns];

            Position posiblePosition = new Position(0, 0);

            // UP
            posiblePosition.DefineValues(Position.Line - 1, Position.Column);
            while(Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;

                if (Board.GetPiece(posiblePosition) != null && Board.GetPiece(posiblePosition).Color != Color)
                {
                    break;
                }
                posiblePosition.Line -= 1;
            }

            // DOWN
            posiblePosition.DefineValues(Position.Line + 1, Position.Column);
            while (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;

                if (Board.GetPiece(posiblePosition) != null && Board.GetPiece(posiblePosition).Color != Color)
                {
                    break;
                }
                posiblePosition.Line += 1;
            }

            // RIGHT
            posiblePosition.DefineValues(Position.Line, Position.Column + 1);
            while (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;

                if (Board.GetPiece(posiblePosition) != null && Board.GetPiece(posiblePosition).Color != Color)
                {
                    break;
                }
                posiblePosition.Column += 1;
            }

            // LEFT
            posiblePosition.DefineValues(Position.Line, Position.Column - 1);
            while (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;

                if (Board.GetPiece(posiblePosition) != null && Board.GetPiece(posiblePosition).Color != Color)
                {
                    break;
                }
                posiblePosition.Column -= 1;
            }

            return posibleMovements;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
