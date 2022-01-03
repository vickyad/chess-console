using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console.chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PosibleMovements()
        {
            bool[,] posibleMovements = new bool[Board.Lines, Board.Columns];

            Position posiblePosition = new Position(0, 0);

            // UP
            posiblePosition.DefineValues(Position.Line - 1, Position.Column);
            if (Board.IsValidPosition(posiblePosition) && CanMoveTo(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // UP-RIGHT
            posiblePosition.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.IsValidPosition(posiblePosition) && CanMoveTo(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // RIGHT
            posiblePosition.DefineValues(Position.Line, Position.Column + 1);
            if (Board.IsValidPosition(posiblePosition) && CanMoveTo(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // DOWN-RIGHT
            posiblePosition.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.IsValidPosition(posiblePosition) && CanMoveTo(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // DOWN
            posiblePosition.DefineValues(Position.Line, Position.Column);
            if (Board.IsValidPosition(posiblePosition) && CanMoveTo(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // DOWN-LEFT
            posiblePosition.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.IsValidPosition(posiblePosition) && CanMoveTo(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // LEFT
            posiblePosition.DefineValues(Position.Line, Position.Column - 1);
            if (Board.IsValidPosition(posiblePosition) && CanMoveTo(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // UP-LEFT
            posiblePosition.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.IsValidPosition(posiblePosition) && CanMoveTo(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            return posibleMovements;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
