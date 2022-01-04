using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console.chess
{
    class ChessMatch
    {
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPieces;
        
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
            _pieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
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

            if (capturedPiece != null)
            {
                _capturedPieces.Add(capturedPiece);
            }
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

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in _capturedPieces)
            {
                if(piece.Color == color)
                {
                    aux.Add(piece);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in _pieces)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void PlaceOnePiece(int line, char column, Piece piece)
        {
            Board.PlacePiece(piece, new ChessPosition(line, column).ToPosition());
            _pieces.Add(piece);
        }

        private void PlacePieces()
        {
            PlaceOnePiece(1, 'c', new Rook(Board, Color.White));
            PlaceOnePiece(2, 'c', new Rook(Board, Color.White));
            PlaceOnePiece(2, 'd', new Rook(Board, Color.White));
            PlaceOnePiece(2, 'e', new Rook(Board, Color.White));
            PlaceOnePiece(1, 'e', new Rook(Board, Color.White));
            PlaceOnePiece(1, 'd', new King(Board, Color.White));

            PlaceOnePiece(7, 'c', new Rook(Board, Color.Black));
            PlaceOnePiece(8, 'c', new Rook(Board, Color.Black));
            PlaceOnePiece(7, 'd', new Rook(Board, Color.Black));
            PlaceOnePiece(7, 'e', new Rook(Board, Color.Black));
            PlaceOnePiece(8, 'e', new Rook(Board, Color.Black));
            PlaceOnePiece(8, 'd', new King(Board, Color.Black));
        }
    }
}
