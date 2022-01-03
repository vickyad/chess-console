using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;

namespace chess_console
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Columns; i++)
            {
                for (int j = 0; j < board.Lines; j++)
                {
                    Piece piece = board.GetPiece(i, j);
                    Console.Write(piece == null ? "- " : $"{piece} ");
                }
                Console.WriteLine();
            }
        }
    }
}
