using board;
using game;
using System.Collections.Generic;

namespace screen;

class Screen
{
    public static void printMatch(Match match)
    {
        Screen.printBoard(match.board);
        Console.WriteLine();
        printCapturedPieces(match);
        Console.WriteLine();
        Console.WriteLine($"Turn: {match.turn}");
        if (match.finished)
        {
            Console.WriteLine("Checkmate!");
            Console.WriteLine($"Winner: {match.actualPlayer}!");
        }
        else
        {
            Console.WriteLine($"Waiting for next player: {match.actualPlayer}");
            Console.WriteLine();
            if (match.check)
                Console.WriteLine("Check!");
            Console.WriteLine();
        }
    }

    public static void printCapturedPieces(Match match)
    {
        Console.WriteLine("Captured pieces: ");
        Console.Write("White: ");
        printGroup(match.checkCapturedPieces(Color.White));
        Console.WriteLine();
        Console.Write("Yellow: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        printGroup(match.checkCapturedPieces(Color.Yellow));
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }

    public static void printGroup(HashSet<Piece> group)
    {
        Console.Write("[");
        foreach (Piece x in group)
            Console.Write($"{x}, ");

        Console.Write("]");
    }

    public static void printBoard(Board board)
    {
        ConsoleColor aux = Console.ForegroundColor;

        for (int i = 0; i < board.rows; i++)
        {
            Console.Write(8 - i + " ");

            for (int j = 0; j < board.columns; j++)

                printPiece(board.piece(i, j), aux);

            Console.WriteLine();
        }

        Console.WriteLine("  A B C D E F G H");
    }

    public static void printBoard(Board board, bool[,] possiblePositions)
    {
        ConsoleColor aux = Console.ForegroundColor;

        ConsoleColor originalBackground = Console.BackgroundColor;
        ConsoleColor possibleSteps = ConsoleColor.White;

        for (int i = 0; i < board.rows; i++)
        {
            Console.Write(8 - i + " ");

            for (int j = 0; j < board.columns; j++)
            {
                if (possiblePositions[i, j])
                {
                    Console.BackgroundColor = possibleSteps;
                }
                else
                {
                    Console.BackgroundColor = originalBackground;
                }

                printPiece(board.piece(i, j), aux);
                Console.BackgroundColor = originalBackground;
            }

            Console.WriteLine();
        }

        Console.WriteLine("  A B C D E F G H");
        Console.BackgroundColor = originalBackground;
    }

    public static void printPiece(Piece piece, ConsoleColor aux)
    {
        var print = "-";

        if (piece != null)
            print = piece.ToString();

        Console.ForegroundColor = piece?.color == Color.Yellow ? ConsoleColor.Yellow : aux;
        Console.Write($"{print} ");
        Console.ForegroundColor = aux;
    }

    public static BoardPosition readBoardPosition()
    {
        string s = Console.ReadLine();
        if (s.Length != 2) 
            throw new BoardException("Invalid piece!");

        char column = s[0];
        int row = int.Parse($"{s[1]}");
        return new BoardPosition(column.ToString(), row);
    }
}
