using board;
using board.pieces;

namespace game;

public class Match
{
    public Board board { get; private set; }
    private int turn;
    private Color actualPlayer;
    public bool finished;

    public Match()
    {
        board = new Board(8, 8);
        turn = 1;
        actualPlayer = Color.White;
        finished = false;
        placePieces();
    }

    public void makeNewMove(Position origin, Position destiny)
    {
        Piece piece = board.removePiece(origin);
        piece.addMoveQuantity();
        Piece grabbedPiece = board.removePiece(destiny);
        board.placePiece(piece, destiny);
    }

    private void placePieces()
    {
        board.placePiece(new Tower(board, Color.White), new BoardPosition("c", 4).toPosition());
    }
}
