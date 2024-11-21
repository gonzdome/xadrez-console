using board;
using System.Runtime.Intrinsics.X86;

namespace screen;

public class Screen
{
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

        Console.Write("  A B C D E F G H");
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
}
