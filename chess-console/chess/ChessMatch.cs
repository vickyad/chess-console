using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console.chess
{
    class ChessMatch
    {
        public Color CurrentPlayer { get; private set; }
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public bool Ended { get; private set; }

        public ChessMatch()
        {
            Turn = 1;
            CurrentPlayer = Color.White;
            Board = new Board(8, 8);
            Ended = false;
            PlacePieces();
        }

        private void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        public void PlayTurn(Position origin, Position destiny)
        {
            MovePiece(origin, destiny);
            Turn++;
            ChangePlayer();
        }

        public void MovePiece(Position origin, Position destiny)
        {
            Piece pieceInMoviment = Board.RemovePiece(origin);
            pieceInMoviment.IncrementMovementsCount();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(pieceInMoviment, destiny);
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.GetPiece(position) == null)
            {
                throw new BoardException("Can't choose a place without a piece");
            }
            if (Board.GetPiece(position).Color != CurrentPlayer)
            {
                throw new BoardException($"It's {CurrentPlayer} turn. Choose the correct color");
            }
            if (!Board.GetPiece(position).HasPosibleMovements())
            {
                throw new BoardException("There is no available movements for this piece");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.GetPiece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Invalid destiny position");
            }
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
