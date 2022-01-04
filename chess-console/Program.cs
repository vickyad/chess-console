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
                ChessMatch match = new ChessMatch();

                while (!match.Ended)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(match.Board);

                        Console.WriteLine($"\nTurn: {match.Turn}");
                        Console.WriteLine($"Current player: {match.CurrentPlayer}");

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] posibleMovements = match.Board.GetPiece(origin).PosibleMovements();
                        Console.Clear();
                        Screen.PrintBoard(match.Board, posibleMovements);

                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDestinyPosition(origin, destiny);

                        match.PlayTurn(origin, destiny);
                    }
                    catch (BoardException exception)
                    {
                        Console.WriteLine(exception.Message);
                        Console.ReadLine();
                    }
                }

                Screen.PrintBoard(match.Board);
            } 
            catch (BoardException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
