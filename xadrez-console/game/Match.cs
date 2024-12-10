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

    public void validateOriginPosition(Position position)
    {
        if (board.piece(position) == null)
            throw new BoardException("There is no piece in the chosen origin!");

        if (actualPlayer != board.piece(position).color)
            throw new BoardException("The chosen origin piece is not yours!");

        if (!board.piece(position).hasPossibleMoves())
            throw new BoardException("There is no possible moves to the chosen origin piece!");
    }

    public void validateDestinyPosition(Position origin, Position destiny)
    {
        if (!board.piece(origin).canMoveTo(destiny))
            throw new BoardException("Invalid destiny position!");
    }

    private void placePieces()
    {
        board.placePiece(new Tower(board, Color.White), new BoardPosition("C", 1).toPosition());
        board.placePiece(new Tower(board, Color.White), new BoardPosition("C", 2).toPosition());
        board.placePiece(new Tower(board, Color.White), new BoardPosition("D", 2).toPosition());
        board.placePiece(new Tower(board, Color.White), new BoardPosition("E", 2).toPosition());
        board.placePiece(new Tower(board, Color.White), new BoardPosition("E", 1).toPosition());
        board.placePiece(new King(board, Color.White), new BoardPosition("D", 1).toPosition());

        board.placePiece(new Tower(board, Color.Yellow), new BoardPosition("C", 8).toPosition());
        board.placePiece(new Tower(board, Color.Yellow), new BoardPosition("C", 7).toPosition());
        board.placePiece(new Tower(board, Color.Yellow), new BoardPosition("D", 7).toPosition());
        board.placePiece(new Tower(board, Color.Yellow), new BoardPosition("E", 7).toPosition());
        board.placePiece(new Tower(board, Color.Yellow), new BoardPosition("E", 8).toPosition());
        board.placePiece(new King(board, Color.Yellow), new BoardPosition("D", 8).toPosition());
    }
}
