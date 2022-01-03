using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console.chess
{
    class ChessMatch
    {
        private int _turn;
        private Color _currentPlayer;
        public Board Board { get; private set; }
        public bool Ended { get; private set; }

        public ChessMatch()
        {
            _turn = 1;
            _currentPlayer = Color.White;
            Board = new Board(8, 8);
            Ended = false;
            PlacePieces();
        }

        public void MovePiece(Position origin, Position destiny)
        {
            Piece pieceInMoviment = Board.RemovePiece(origin);
            pieceInMoviment.IncrementMovementsCount();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(pieceInMoviment, destiny);
        }

        private void PlacePieces()
        {
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition(1, 'c').ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition(2, 'c').ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition(2, 'd').ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition(2, 'e').ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition(1, 'e').ToPosition());
            Board.PlacePiece(new King(Board, Color.White), new ChessPosition(1, 'd').ToPosition());

            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition(7, 'c').ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition(8, 'c').ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition(7, 'd').ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition(7, 'e').ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition(8, 'e').ToPosition());
            Board.PlacePiece(new King(Board, Color.Black), new ChessPosition(8, 'd').ToPosition());
        }
    }
}
