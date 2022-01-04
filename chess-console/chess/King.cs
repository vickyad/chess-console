using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console.chess
{
    class King : Piece
    {
        private ChessMatch _match;
        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match; 
        }

        private bool RookAvailableForCastling(Position position)
        {
            Piece rookForPlay = Board.GetPiece(position);
            if (rookForPlay != null && rookForPlay is Rook && rookForPlay.Color == Color && rookForPlay.MovementsCount == 0)
            {
                return true;
            }
            return false;
        }

        public override bool[,] PosibleMovements()
        {
            bool[,] posibleMovements = new bool[Board.Lines, Board.Columns];

            Position posiblePosition = new Position(0, 0);

            // UP
            posiblePosition.DefineValues(Position.Line - 1, Position.Column);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // UP-RIGHT
            posiblePosition.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // RIGHT
            posiblePosition.DefineValues(Position.Line, Position.Column + 1);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // DOWN-RIGHT
            posiblePosition.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // DOWN
            posiblePosition.DefineValues(Position.Line, Position.Column);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // DOWN-LEFT
            posiblePosition.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // LEFT
            posiblePosition.DefineValues(Position.Line, Position.Column - 1);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // UP-LEFT
            posiblePosition.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.IsValidPosition(posiblePosition) && IsValidMove(posiblePosition))
            {
                posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
            }

            // Special Play: Castling
            if (MovementsCount == 0 && !_match.Check)
            {
                // Short-side Castling
                if (RookAvailableForCastling(new Position(Position.Line, Position.Column + 3)) && Board.GetPiece(Position.Line, Position.Column + 1) == null && Board.GetPiece(Position.Line, Position.Column + 2) == null)
                {
                    posibleMovements[Position.Line, Position.Column + 2] = true;
                }

                // Long-side Castling
                if (RookAvailableForCastling(new Position(Position.Line, Position.Column - 4)) && Board.GetPiece(Position.Line, Position.Column - 1) == null && Board.GetPiece(Position.Line, Position.Column - 2) == null && Board.GetPiece(Position.Line, Position.Column - 3) == null)
                {
                    posibleMovements[Position.Line, Position.Column - 2] = true;
                }
            }

            return posibleMovements;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
