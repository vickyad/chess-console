using System;
using System.Collections.Generic;
using System.Text;

namespace chess_console.board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] _pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            _pieces = new Piece[lines, columns];
        }

        public Piece GetPiece(int line, int column)
        {
            return _pieces[line, column];
        }

        public Piece GetPiece(Position position)
        {
            return _pieces[position.Line, position.Column];
        }

        public bool HasPieceInPosition(Position position)
        {
            ValidatePosition(position);
            return GetPiece(position) != null;
        }

        public void PlacePiece(Piece piece, Position position)
        {
            if (HasPieceInPosition(position))
            {
                throw new BoardException("There is already a piece placed in this position");
            }
            _pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (GetPiece(position) == null)
            {
                return null;
            }
            Piece removedPiece = GetPiece(position);
            removedPiece.Position = null;
            _pieces[position.Line, position.Column] = null;
            return removedPiece;
        }

        public bool IsValidPosition(Position position)
        {
            if (position.Line < 0 || position.Line > Lines || position.Column < 0 || position.Column > Columns)
            {
                return false;
            }
            return true;
        } 

        public void ValidatePosition(Position position)
        {
            if (!IsValidPosition(position))
            {
                throw new BoardException("Invalid position");
            }
        }
    }
}
