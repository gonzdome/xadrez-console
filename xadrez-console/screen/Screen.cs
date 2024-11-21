using board;
using System.ComponentModel;
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

        Console.WriteLine("  A B C D E F G H");
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
        char column = s[0];
        int row = int.Parse($"{s[1]}");
        return new BoardPosition(column.ToString(), row);
    }
}
