using board;
using board.pieces;
using System.Collections.Generic;
namespace game;

class Match
{
    public Board board { get; private set; }
    public int turn { get; private set; }
    public Color actualPlayer { get; private set; }
    public bool finished { get; private set; }
    private HashSet<Piece> pieces;
    private HashSet<Piece> capturedPieces;

    public Match()
    {
        board = new Board(8, 8);
        turn = 1;
        actualPlayer = Color.White;
        finished = false;
        pieces = new HashSet<Piece>();
        capturedPieces = new HashSet<Piece>();
        placePieces();
    }

    public void makeNewMove(Position origin, Position destiny)
    {
        Piece piece = board.removePiece(origin);
        if (piece == null)
            throw new BoardException("Piece not found!");

        piece.addMoveQuantity();
        Piece capturedPiece = board.removePiece(destiny);
        board.placePiece(piece, destiny);
        if (capturedPiece != null)
            capturedPieces.Add(capturedPiece);
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

    public HashSet<Piece> checkCapturedPieces(Color color)
    {
        HashSet<Piece> aux = new HashSet<Piece>();
        foreach (Piece x in capturedPieces)
        {
            if (x.color == color)
                aux.Add(x);
        }

        return aux;
    }

    public HashSet<Piece> pieceInGame(Color color)
    {
        HashSet<Piece> aux = new HashSet<Piece>();
        foreach (Piece x in pieces)
        {
            if (x.color == color)
                aux.Add(x);
        }

        aux.ExceptWith(checkCapturedPieces(color));
        return aux;
    }

    public void placeNewPiece(string column, int row, Piece piece)
    {
        board.placePiece(piece, new BoardPosition(column, row).toPosition());
        pieces.Add(piece);
    }

    private void placePieces()
    {
        placeNewPiece("C", 1, new Tower(board, Color.White));
        placeNewPiece("C", 2, new Tower(board, Color.White));
        placeNewPiece("D", 2, new Tower(board, Color.White));
        placeNewPiece("E", 2, new Tower(board, Color.White));
        placeNewPiece("E", 1, new Tower(board, Color.White));
        placeNewPiece("D", 1, new King(board, Color.White));

        placeNewPiece("C", 8, new Tower(board, Color.Yellow));
        placeNewPiece("C", 7, new Tower(board, Color.Yellow));
        placeNewPiece("D", 7, new Tower(board, Color.Yellow));
        placeNewPiece("E", 7, new Tower(board, Color.Yellow));
        placeNewPiece("E", 8, new Tower(board, Color.Yellow));
        placeNewPiece("D", 8, new King(board, Color.Yellow));
    }
}
