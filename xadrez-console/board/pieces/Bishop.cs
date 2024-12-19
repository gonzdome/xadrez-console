namespace board.pieces;

class Bishop : Piece
{
    public Bishop(Board board, Color color) : base(board, color)
    {
    }

    public override string ToString()
    {
        return "B";
    }
    private bool possibleMove(Position position)
    {
        Piece piece = board.piece(position);
        return piece == null || piece.color != color;
    }

    public override bool[,] possibleMoves()
    {
        bool[,] boolMat = new bool[board.rows, board.columns];

        Position newPosition = new Position(0, 0);

        // NO
        newPosition.defineValues(position.row - 1, position.column - 1);
        while (board.validPosition(newPosition) && possibleMove(newPosition)) 
        {
            boolMat[newPosition.row, newPosition.column] = true;
            if(board.piece(newPosition) != null && board.piece(newPosition).color != color)
                break;

            newPosition.defineValues(newPosition.row - 1, newPosition.column -1);
        }

        // NE
        newPosition.defineValues(position.row + 1, position.column + 1);
        while (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
            if (board.piece(newPosition) != null && board.piece(newPosition).color != color)
                break;

            newPosition.defineValues(newPosition.row + 1, newPosition.column + 1);
        }

        // SE
        newPosition.defineValues(position.row + 1, position.column - 1);
        while (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
            if (board.piece(newPosition) != null && board.piece(newPosition).color != color)
                break;

            newPosition.defineValues(newPosition.row + 1, newPosition.column - 1);
        }

        // SO
        newPosition.defineValues(position.row - 1, position.column + 1);
        while (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
            if (board.piece(newPosition) != null && board.piece(newPosition).color != color)
                break;

            newPosition.defineValues(newPosition.row - 1, newPosition.column + 1);
        }

        return boolMat;
    }
}
