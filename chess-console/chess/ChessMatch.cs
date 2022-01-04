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
        public bool Check { get; private set; }


        public ChessMatch()
        {
            Turn = 1;
            CurrentPlayer = Color.White;
            Board = new Board(8, 8);
            Ended = false;
            Check = false;
            _pieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            PlacePieces();
        }

        private void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        private Color Opponent(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        private Piece GetKing(Color color)
        {
            foreach (Piece piece in PiecesInGame(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = GetKing(color);
            if (king == null)
            {
                throw new BoardException($"There is no {color} king in the board");
            }

            foreach (Piece piece in PiecesInGame(Opponent(color)))
            {
                bool[,] posibleMovements = piece.PosibleMovements();
                if (posibleMovements[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsInCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] posibleMovements = piece.PosibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (posibleMovements[i, j])
                        {
                            Position origin = piece.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = DoMovement(origin, destiny);
                            bool check = IsInCheck(color);
                            UndoMovement(origin, destiny, capturedPiece);

                            if (!check)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PlayTurn(Position origin, Position destiny)
        {
            Piece capturedPiece = DoMovement(origin, destiny);
            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, capturedPiece);
                throw new BoardException("You cannot put yourself in check");
            }

            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (IsInCheckMate(Opponent(CurrentPlayer)))
            {
                Ended = true;
            } 
            else
            {
                Turn++;
                ChangePlayer();
            }
        }

        public Piece DoMovement(Position origin, Position destiny)
        {
            Piece pieceInMoviment = Board.RemovePiece(origin);
            pieceInMoviment.IncrementMovementsCount();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(pieceInMoviment, destiny);

            if (capturedPiece != null)
            {
                _capturedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(destiny);
            piece.DecrementMovementCount();

            if (capturedPiece != null)
            {
                Board.PlacePiece(capturedPiece, destiny);
                _capturedPieces.Remove(capturedPiece);
            }
            Board.PlacePiece(piece, origin);
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
            /*
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
            */
            
            PlaceOnePiece(1, 'c', new Rook(Board, Color.White));
            PlaceOnePiece(1, 'd', new King(Board, Color.White));
            PlaceOnePiece(7, 'h', new Rook(Board, Color.White));

            PlaceOnePiece(8, 'a', new King(Board, Color.Black));
            PlaceOnePiece(8, 'b', new Rook(Board, Color.Black));
        }
    }
}
