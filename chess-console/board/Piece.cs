using System;
using System.Collections.Generic;
using System.Text;

namespace chess_console.board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementsCount { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Board = board;
            Color = color;
            Position = null;
            MovementsCount = 0;
        }
        protected bool IsValidMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        public void IncrementMovementsCount()
        {
            MovementsCount++;
        }

        public bool HasPosibleMovements()
        {
            bool[,] posibleMoviments = PosibleMovements();
            for(int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (posibleMoviments[i, j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position position)
        {
            return PosibleMovements()[position.Line, position.Column];
        }

        public abstract bool[,] PosibleMovements();
    }
}
