using System;
using chess_console.board;
using chess_console.chess;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);
            
                board.PlacePiece(new Rook(board, Color.Black), new Position(0, 0));
                board.PlacePiece(new Rook(board, Color.Black), new Position(1, 3));
                board.PlacePiece(new King(board, Color.Black), new Position(0, 2));
            
                Screen.PrintBoard(board);
            } 
            catch (BoardException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
