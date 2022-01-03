﻿using System;
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

        public void PlacePiece(Piece piece, Position position)
        {
            _pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }
    }
}
