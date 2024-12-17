namespace board.pieces;

class Horse : Piece
{
    public Horse(Board board, Color color) : base(board, color)
    {
    }

    public override string ToString()
    {
        return "H";
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

        newPosition.defineValues(position.row - 1, position.column - 2);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        newPosition.defineValues(position.row - 2, position.column - 1);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        newPosition.defineValues(position.row -2, position.column + 1);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        newPosition.defineValues(position.row - 1, position.column + 2);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        newPosition.defineValues(position.row + 1, position.column + 2);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        newPosition.defineValues(position.row + 2, position.column + 1);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        newPosition.defineValues(position.row + 2, position.column - 1);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        newPosition.defineValues(position.row + 1, position.column - 2);
        if (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
        }

        return boolMat;
    }
}
