using System;
using chess_console.board;
using chess_console.chess;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition position = new ChessPosition(1, 'a');
            Console.WriteLine(position);
            Console.WriteLine(position.ToPosition());
        }
    }
}
