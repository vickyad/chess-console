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
            bool[,] posibleMovements = new bool[Board.Lines, Board.Columns];

            Position posiblePosition = new Position(0, 0);

            posiblePosition.DefineValues(Position.Line - 1, Position.Column - 2);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            posiblePosition.DefineValues(Position.Line - 2, Position.Column - 1);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            posiblePosition.DefineValues(Position.Line - 2, Position.Column + 1);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            posiblePosition.DefineValues(Position.Line - 1, Position.Column + 2);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            posiblePosition.DefineValues(Position.Line + 1, Position.Column + 2);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            posiblePosition.DefineValues(Position.Line + 2, Position.Column + 1);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            posiblePosition.DefineValues(Position.Line + 2, Position.Column - 1);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            posiblePosition.DefineValues(Position.Line + 1, Position.Column - 2);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            return posibleMovements;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
