using board;
using board.pieces;

namespace game;

class Match
{
    public Board board { get; private set; }
    public int turn { get; private set; }
    public Color actualPlayer { get; private set; }
    public bool finished { get; private set; }

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
        if (piece == null)
            throw new BoardException("Piece not found!");

        piece.addMoveQuantity();
        Piece grabbedPiece = board.removePiece(destiny);
        board.placePiece(piece, destiny);
    }

    private void changePlayer()
    {
        if (actualPlayer == Color.White)
            actualPlayer = Color.Yellow;
        else
            actualPlayer = Color.White;
    }

    public void executePlay(Position origin, Position destiny)
    {
        makeNewMove(origin, destiny);
        turn++;
        changePlayer();
    }

    private void placePieces()
    {
        board.placePiece(new Tower(board, Color.White), new BoardPosition("C", 4).toPosition());
        board.placePiece(new King(board, Color.White), new BoardPosition("E", 1).toPosition());
    }
}
