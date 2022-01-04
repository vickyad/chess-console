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

        private bool HasEnemy(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece != null && piece.Color != Color;
        } 

        private bool IsEmpty(Position position)
        {
            return Board.GetPiece(position) == null;
        }

        public override bool[,] PosibleMovements()
        {
            bool[,] posibleMovements = new bool[Board.Lines, Board.Columns];

            Position posiblePosition = new Position(0, 0);

            if (Color == Color.White)
            {
                posiblePosition.DefineValues(Position.Line - 1, Position.Column);
                if (Board.IsValidPosition(posiblePosition) && IsEmpty(posiblePosition))
                {
                    posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
                
                    posiblePosition.DefineValues(Position.Line - 2, Position.Column);
                    if (Board.IsValidPosition(posiblePosition) && IsEmpty(posiblePosition) && MovementsCount == 0)
                    {
                        posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
                    }
                }

                posiblePosition.DefineValues(Position.Line - 1, Position.Column - 1);
                if (Board.IsValidPosition(posiblePosition) && HasEnemy(posiblePosition))
                {
                    posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
                }

                posiblePosition.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Board.IsValidPosition(posiblePosition) && HasEnemy(posiblePosition))
                {
                    posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
                }
            }
            else
            {
                posiblePosition.DefineValues(Position.Line + 1, Position.Column);
                if (Board.IsValidPosition(posiblePosition) && IsEmpty(posiblePosition))
                {
                    posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;

                    posiblePosition.DefineValues(Position.Line + 2, Position.Column);
                    if (Board.IsValidPosition(posiblePosition) && IsEmpty(posiblePosition) && MovementsCount == 0)
                    {
                        posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
                    }
                }

                posiblePosition.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.IsValidPosition(posiblePosition) && HasEnemy(posiblePosition))
                {
                    posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
                }

                posiblePosition.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.IsValidPosition(posiblePosition) && HasEnemy(posiblePosition))
                {
                    posibleMovements[posiblePosition.Line, posiblePosition.Column] = true;
                }
            }

            return posibleMovements;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
