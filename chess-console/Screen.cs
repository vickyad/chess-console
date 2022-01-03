﻿using System;
using System.Collections.Generic;
using System.Text;
using chess_console.board;
using chess_console.chess;

namespace chess_console
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Columns; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Lines; j++)
                {
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] posibleMovements)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor markedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Columns; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Lines; j++)
                {
                    Console.BackgroundColor = posibleMovements[i, j] ? markedBackground : originalBackground;
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
                Console.BackgroundColor = originalBackground;
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            } 
            else
            { 
                if(piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else {
                    ConsoleColor currentForegroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(piece);
                    Console.ForegroundColor = currentForegroundColor;
                }
                Console.Write(" ");
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string input = Console.ReadLine();
            char column = input[0];
            int line = int.Parse(input[1].ToString());
            return new ChessPosition(line, column);
        }
    }
}
