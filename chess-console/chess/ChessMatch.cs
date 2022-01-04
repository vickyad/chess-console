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
        public Piece VulnerableToEnPassant { get; private set; }

        public ChessMatch()
        {
            VulnerableToEnPassant = null;
            _capturedPieces = new HashSet<Piece>();
            _pieces = new HashSet<Piece>();
            Turn = 1;
            CurrentPlayer = Color.White;
            Board = new Board(8, 8);
            Ended = false;
            Check = false;
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
            
            Piece movedPiece = Board.GetPiece(destiny);
            
            // Special Play: Promotion
            if (movedPiece is Pawn)
            {
                if ((movedPiece.Color == Color.White && destiny.Line == 0) || (movedPiece.Color == Color.Black && destiny.Line == 7))
                {
                    movedPiece = Board.RemovePiece(destiny);
                    _pieces.Remove(movedPiece);
                    Piece queen = new Queen(Board, movedPiece.Color);
                    Board.PlacePiece(queen, destiny);
                    _pieces.Add(queen);
                }
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
            

                // Special Play: En Passant
                if (movedPiece is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
                {
                    VulnerableToEnPassant = movedPiece;
                } else
                {
                    VulnerableToEnPassant = null;
                }
            }
        }

        public Piece DoMovement(Position origin, Position destiny)
        {
            Piece pieceInMovement = Board.RemovePiece(origin);
            pieceInMovement.IncrementMovementsCount();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(pieceInMovement, destiny);

            if (capturedPiece != null)
            {
                _capturedPieces.Add(capturedPiece);
            }

            // Special Play: Short-side Castling
            if (pieceInMovement is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestiny = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementMovementsCount();
                Board.PlacePiece(rook, rookDestiny);
            }

            // Special Play: Long-side Castling
            if (pieceInMovement is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestiny = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementMovementsCount();
                Board.PlacePiece(rook, rookDestiny);
            }

            // Special Play: En Passant
            if (pieceInMovement is Pawn && origin.Column != destiny.Column && capturedPiece == null)
            {
                Position pawnPosition = new Position(pieceInMovement.Color == Color.White ? destiny.Line + 1 : destiny.Line - 1, destiny.Column);
                capturedPiece = Board.RemovePiece(pawnPosition);
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

            // Special Play: Short-side Castling
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestiny = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookDestiny);
                rook.DecrementMovementCount();
                Board.PlacePiece(rook, rookOrigin);
            }

            // Special Play: Long-side Castling
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestiny = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookDestiny);
                rook.DecrementMovementCount();
                Board.PlacePiece(rook, rookOrigin);
            }

            // Special Play: En Passant
            if (piece is Pawn && origin.Column != destiny.Column && capturedPiece == VulnerableToEnPassant)
            {
                Piece pawn = Board.RemovePiece(destiny);
                Position pawnPosition = new Position(piece.Color == Color.White ? 3 : 4, destiny.Column);
                Board.PlacePiece(pawn, pawnPosition);
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
            PlaceOnePiece(1, 'a', new Rook(Board, Color.White));
            PlaceOnePiece(1, 'b', new Knight(Board, Color.White));
            PlaceOnePiece(1, 'c', new Bishop(Board, Color.White));
            PlaceOnePiece(1, 'd', new Queen(Board, Color.White));
            PlaceOnePiece(1, 'e', new King(Board, Color.White, this));
            PlaceOnePiece(1, 'f', new Bishop(Board, Color.White));
            PlaceOnePiece(1, 'g', new Knight(Board, Color.White));
            PlaceOnePiece(1, 'h', new Rook(Board, Color.White));
            PlaceOnePiece(2, 'a', new Pawn(Board, Color.White, this));
            PlaceOnePiece(2, 'b', new Pawn(Board, Color.White, this));
            PlaceOnePiece(2, 'c', new Pawn(Board, Color.White, this));
            PlaceOnePiece(2, 'd', new Pawn(Board, Color.White, this));
            PlaceOnePiece(2, 'e', new Pawn(Board, Color.White, this));
            PlaceOnePiece(2, 'f', new Pawn(Board, Color.White, this));
            PlaceOnePiece(2, 'g', new Pawn(Board, Color.White, this));
            PlaceOnePiece(2, 'h', new Pawn(Board, Color.White, this));

            PlaceOnePiece(8, 'a', new Rook(Board, Color.Black));
            PlaceOnePiece(8, 'b', new Knight(Board, Color.Black));
            PlaceOnePiece(8, 'c', new Bishop(Board, Color.Black));
            PlaceOnePiece(8, 'd', new Queen(Board, Color.Black));
            PlaceOnePiece(8, 'e', new King(Board, Color.Black, this));
            PlaceOnePiece(8, 'f', new Bishop(Board, Color.Black));
            PlaceOnePiece(8, 'g', new Knight(Board, Color.Black));
            PlaceOnePiece(8, 'h', new Rook(Board, Color.Black));
            PlaceOnePiece(7, 'a', new Pawn(Board, Color.Black, this));
            PlaceOnePiece(7, 'b', new Pawn(Board, Color.Black, this));
            PlaceOnePiece(7, 'c', new Pawn(Board, Color.Black, this));
            PlaceOnePiece(7, 'd', new Pawn(Board, Color.Black, this));
            PlaceOnePiece(7, 'e', new Pawn(Board, Color.Black, this));
            PlaceOnePiece(7, 'f', new Pawn(Board, Color.Black, this));
            PlaceOnePiece(7, 'g', new Pawn(Board, Color.Black, this));
            PlaceOnePiece(7, 'h', new Pawn(Board, Color.Black, this));
        }
    }
}
