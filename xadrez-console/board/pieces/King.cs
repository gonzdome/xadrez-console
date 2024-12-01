namespace board.pieces;

class King : Piece
{
    public King(Board board, Color color) : base(board, color)
    {
    }

    public override string ToString()
    {
        return "R";
    }

    private bool possibleMove(Position position)
    {
        Piece piece = board.piece(position);
        return piece == null || piece.color != this.color;
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

        return boolMat;
    }
}
