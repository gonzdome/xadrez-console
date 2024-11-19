using board;
using board.pieces;
using screen;

namespace xadrez_console;

class Program
{
    static void Main(string[] args)
    {
        Board board = new Board(8, 8);

        board.PlacePiece(new Tower(board, Color.Black), new Position(0, 0));
        board.PlacePiece(new Tower(board, Color.Black), new Position(0, 7));
        board.PlacePiece(new King(board, Color.Black), new Position(0, 4));

        Screen.printBoard(board);
    }
}
