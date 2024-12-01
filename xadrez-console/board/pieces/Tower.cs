namespace board.pieces;

class Tower : Piece
{
    public Tower(Board board, Color color) : base(board, color)
    {
    }

    public override string ToString()
    {
        return "T";
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
        while (board.validPosition(newPosition) && possibleMove(newPosition)) 
        {
            boolMat[newPosition.row, newPosition.column] = true;
            if(board.piece(newPosition) != null && board.piece(newPosition).color != color)
                break;

            newPosition.row -= 1;
        }

        // Down
        newPosition.defineValues(position.row + 1, position.column);
        while (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
            if (board.piece(newPosition) != null && board.piece(newPosition).color != color)
                break;

            newPosition.row += 1;
        }

        // Right
        newPosition.defineValues(position.row, position.column - 1);
        while (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
            if (board.piece(newPosition) != null && board.piece(newPosition).color != color)
                break;

            newPosition.column -= 1;
        }

        // Left
        newPosition.defineValues(position.row, position.column + 1);
        while (board.validPosition(newPosition) && possibleMove(newPosition))
        {
            boolMat[newPosition.row, newPosition.column] = true;
            if (board.piece(newPosition) != null && board.piece(newPosition).color != color)
                break;

            newPosition.column += 1;
        }

        return boolMat;
    }
}
