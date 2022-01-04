using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console.chess
{
    class Pawn : Piece
    {
        private ChessMatch _match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match;
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

                // Special Play: En Passant
                if (Position.Line == 3)
                {
                    Position leftPosition = new Position(Position.Line, Position.Column - 1); 
                    if (Board.IsValidPosition(leftPosition) && HasEnemy(leftPosition) && Board.GetPiece(leftPosition) == _match.VulnerableToEnPassant)
                    {
                        posibleMovements[leftPosition.Line - 1, leftPosition.Column] = true;
                    }

                    Position rightPosition = new Position(Position.Line, Position.Column + 1);
                    if (Board.IsValidPosition(rightPosition) && HasEnemy(rightPosition) && Board.GetPiece(rightPosition) == _match.VulnerableToEnPassant)
                    {
                        posibleMovements[rightPosition.Line - 1, rightPosition.Column] = true;
                    }
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

                // Special Play: En Passant
                if (Position.Line == 4)
                {
                    Position leftPosition = new Position(Position.Line, Position.Column - 1);
                    if (Board.IsValidPosition(leftPosition) && HasEnemy(leftPosition) && Board.GetPiece(leftPosition) == _match.VulnerableToEnPassant)
                    {
                        posibleMovements[leftPosition.Line + 1, leftPosition.Column] = true;
                    }

                    Position rightPosition = new Position(Position.Line, Position.Column + 1);
                    if (Board.IsValidPosition(rightPosition) && HasEnemy(rightPosition) && Board.GetPiece(rightPosition) == _match.VulnerableToEnPassant)
                    {
                        posibleMovements[rightPosition.Line + 1, rightPosition.Column] = true;
                    }
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
