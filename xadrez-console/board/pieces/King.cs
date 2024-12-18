using game;

namespace board.pieces;

class King : Piece
{
    private Match match;
    public King(Board board, Color color, Match match) : base(board, color)
    {
        this.match = match;
    }

    public override string ToString()
    {
        return "K";
    }

    private bool possibleMove(Position position)
    {
        Piece piece = board.piece(position);
        return piece == null || piece.color != this.color;
    }

    private bool canCastle(Position position)
    {
        Piece piece = board.piece(position);
        return piece != null && piece is Rook && piece.color == color && piece.moves == 0;
    }

    public override bool[,] possibleMoves()
    {
        bool[,] boolMat = new bool[board.rows, board.columns];

        Position newPosition = new Position(0, 0);

        // Up
        newPosition.defineValues(position.row - 1, position.column);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        // Up and Right
        newPosition.defineValues(position.row - 1, position.column + 1);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        // Right
        newPosition.defineValues(position.row, position.column + 1);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        // Down and Right
        newPosition.defineValues(position.row + 1, position.column + 1);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        // Down
        newPosition.defineValues(position.row + 1, position.column);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        // Down and Left
        newPosition.defineValues(position.row + 1, position.column - 1);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        // Left
        newPosition.defineValues(position.row, position.column - 1);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        // Up and Left
        newPosition.defineValues(position.row - 1, position.column - 1);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        // Castle
        if(moves == 0 && !match.check)
        {
            Position towerPosition = new Position(position.row, position.column + 3);
            if (canCastle(towerPosition))
            {
                Position position1 = new Position(position.row, position.column + 1);
                Position position2 = new Position(position.row, position.column + 2);
                if(board.piece(position1) == null && board.piece(position2) == null)
                    boolMat [position.row, position.column] = true;
            }

            Position towerPosition2 = new Position(position.row, position.column - 4);
            if (canCastle(towerPosition))
            {
                Position position1 = new Position(position.row, position.column - 1);
                Position position2 = new Position(position.row, position.column - 2);
                Position position3 = new Position(position.row, position.column - 3);
                if (board.piece(position1) == null && board.piece(position2) == null && board.piece(position3) == null)
                    boolMat[position.row, position.column] = true;
            }
        }

        return boolMat;
    }
}
