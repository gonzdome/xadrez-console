﻿using board;
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
    public Piece vulnerableEnPassant { get; private set; }
    public bool check { get; private set; }
    public Match()
    {
        board = new Board(8, 8);
        turn = 1;
        actualPlayer = Color.White;
        finished = false;
        pieces = new HashSet<Piece>();
        capturedPieces = new HashSet<Piece>();
        vulnerableEnPassant = null;
        check = false;
        placePieces();
    }

    public Piece makeNewMove(Position origin, Position destiny)
    {
        Piece piece = board.removePiece(origin);
        if (piece == null)
            throw new BoardException("Piece not found!");

        piece.addMoveQuantity();
        Piece capturedPiece = board.removePiece(destiny);
        board.placePiece(piece, destiny);
        if (capturedPiece != null)
            capturedPieces.Add(capturedPiece);

        // Castle
        if (piece is King && destiny.column == origin.column + 2)
        {
            Position originR = new Position(origin.row, origin.column + 3);
            Position destinyR = new Position(origin.row, origin.column + 1);
            Piece rook = board.removePiece(originR);
            rook.addMoveQuantity();
            board.placePiece(rook, destinyR);
        }

        if (piece is King && destiny.column == origin.column - 2)
        {
            Position originR = new Position(origin.row, origin.column - 4);
            Position destinyR = new Position(origin.row, origin.column - 1);
            Piece rook = board.removePiece(originR);
            rook.addMoveQuantity();
            board.placePiece(rook, destinyR);
        }

        // En Passant
        if (piece is Pawn)
        {
            if (origin.column != destiny.column && capturedPiece == null)
            {
                Position positionP;
                
                if (piece.color == Color.White)
                    positionP = new Position(destiny.row + 1, destiny.column);
                else
                    positionP = new Position(destiny.row - 1, destiny.column);
                
                capturedPiece = board.removePiece(positionP);
                capturedPieces.Add(capturedPiece);
            }
        }

        return capturedPiece;
    }

    public void undoPlay(Position origin, Position destiny, Piece capturedPiece)
    {
        Piece piece = board.removePiece(destiny);
        piece.removeMoveQuantity();
        if (capturedPiece != null)
        {
            board.placePiece(capturedPiece, destiny);
            capturedPieces.Remove(capturedPiece);
        }

        // Castle
        if (piece is King && destiny.column == origin.column + 2)
        {
            Position originR = new Position(origin.row, origin.column + 3);
            Position destinyR = new Position(origin.row, origin.column + 1);
            Piece rook = board.removePiece(destinyR);
            rook.addMoveQuantity();
            board.placePiece(rook, originR);
        }

        if (piece is King && destiny.column == origin.column - 2)
        {
            Position originR = new Position(origin.row, origin.column - 4);
            Position destinyR = new Position(origin.row, origin.column - 1);
            Piece rook = board.removePiece(destinyR);
            rook.addMoveQuantity();
            board.placePiece(rook, originR);
        }

        // En Passant
        if (piece is Pawn)
        {
            if (origin.column != destiny.column && capturedPiece == vulnerableEnPassant)
            {
                Piece pawn = board.removePiece(destiny);
                Position positionP;
                if (pawn.color == Color.White)
                    positionP = new Position(3, destiny.column);
                else
                    positionP = new Position(4, destiny.column);

                board.placePiece(pawn, positionP);
            }
        }

        board.placePiece(piece, origin);
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
        var capturedPiece = makeNewMove(origin, destiny);
        if (isCheck(actualPlayer))
        {
            undoPlay(origin, destiny, capturedPiece);
            throw new BoardException("You're in check!");
        }
        
        Piece piece = board.piece(destiny);
        
        // Pawn Promotion
        if (piece is Pawn)
        {
            if ((piece.color == Color.White && destiny.row == 0) || (piece.color == Color.Yellow && destiny.row == 7))
            {
                piece = board.removePiece(destiny);
                pieces.Remove(piece);
                Piece queen = new Queen(board, piece.color);
                board.placePiece(queen, destiny);
                pieces.Add(queen);
            }
        }

        var getEnemy = enemy(actualPlayer);

        if (isCheck(getEnemy))
            check = true;
        else 
            check = false;

        if (!isCheckMate(getEnemy))
        {
            turn++;
            changePlayer();
        }
        else
            finished = true;

        // En Passant
        if (piece is Pawn && (destiny.row == origin.row - 2 || destiny.row == origin.row + 2))
            vulnerableEnPassant = piece;
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
        if (!board.piece(origin).possibleMove(destiny))
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

    public HashSet<Piece> piecesInGame(Color color)
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

    private Color enemy (Color color)
    {
        if (color == Color.White)
            return Color.Yellow;
        else
            return Color.White;
    }

    private Piece king (Color color)
    {
        foreach (Piece piece in piecesInGame(color))
        {
            if (piece is King)
                return piece;
        }
        return null;
    }

    public bool isCheck(Color color)
    {
        Piece kingPiece = king(color);
        if (kingPiece == null)
            throw new BoardException($"There is no {color} king!");

        foreach (Piece p in piecesInGame(enemy(color)))
        {
            bool[,] possibleMoves = p.possibleMoves();
            if (possibleMoves[kingPiece.position.row, kingPiece.position.column])
                return true;
        }
        return false;
    }

    public bool isCheckMate(Color color)
    {
        if (!isCheck(color))
            return false;

        foreach(Piece piece in piecesInGame(color))
        {
            bool[,] mat = piece.possibleMoves();
            for(int i = 0; i < board.rows; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if (mat[i, j])
                    {
                        Position origin = piece.position;
                        Position destiny = new Position(i, j);
                        Piece capturedPiece = makeNewMove(origin, destiny);
                        bool testCheck = isCheck(color);
                        undoPlay(origin, destiny, capturedPiece);
                        if (!testCheck)
                            return false;
                    }
                }
            }
        }
        
        return true;
    }

    public void placeNewPiece(string column, int row, Piece piece)
    {
        board.placePiece(piece, new BoardPosition(column, row).toPosition());
        pieces.Add(piece);
    }

    private void placePieces()
    {
        placeNewPiece("A", 1, new Rook(board, Color.White));
        placeNewPiece("B", 1, new Horse(board, Color.White));
        placeNewPiece("C", 1, new Bishop(board, Color.White));
        placeNewPiece("D", 1, new Queen(board, Color.White));
        placeNewPiece("E", 1, new King(board, Color.White, this));
        placeNewPiece("F", 1, new Bishop(board, Color.White));
        placeNewPiece("G", 1, new Horse(board, Color.White));
        placeNewPiece("H", 1, new Rook(board, Color.White));
        placeNewPiece("A", 2, new Pawn(board, Color.White, this));
        placeNewPiece("B", 2, new Pawn(board, Color.White, this));
        placeNewPiece("C", 2, new Pawn(board, Color.White, this));
        placeNewPiece("D", 2, new Pawn(board, Color.White, this));
        placeNewPiece("E", 2, new Pawn(board, Color.White, this));
        placeNewPiece("F", 2, new Pawn(board, Color.White, this));
        placeNewPiece("G", 2, new Pawn(board, Color.White, this));
        placeNewPiece("H", 2, new Pawn(board, Color.White, this));

        placeNewPiece("A", 8, new Rook(board, Color.Yellow));
        placeNewPiece("B", 8, new Horse(board, Color.Yellow));
        placeNewPiece("C", 8, new Bishop(board, Color.Yellow));
        placeNewPiece("D", 8, new Queen(board, Color.Yellow));
        placeNewPiece("E", 8, new King(board, Color.Yellow, this));
        placeNewPiece("F", 8, new Bishop(board, Color.Yellow));
        placeNewPiece("G", 8, new Horse(board, Color.Yellow));
        placeNewPiece("H", 8, new Rook(board, Color.Yellow));
        placeNewPiece("A", 7, new Pawn(board, Color.Yellow, this));
        placeNewPiece("B", 7, new Pawn(board, Color.Yellow, this));
        placeNewPiece("C", 7, new Pawn(board, Color.Yellow, this));
        placeNewPiece("D", 7, new Pawn(board, Color.Yellow, this));
        placeNewPiece("E", 7, new Pawn(board, Color.Yellow, this));
        placeNewPiece("F", 7, new Pawn(board, Color.Yellow, this));
        placeNewPiece("G", 7, new Pawn(board, Color.Yellow, this));
        placeNewPiece("H", 7, new Pawn(board, Color.Yellow, this));
    }
}
