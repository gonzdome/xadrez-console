namespace board.pieces;

class Pawn : Piece
{
    public Pawn(Board board, Color color) : base(board, color)
    {
    }

    public override string ToString()
    {
        return "P";
    }

    private bool enemyExists(Position position)
    {
        Piece piece = board.piece(position);
        return piece != null && piece.color != color;
    }

    private bool free(Position position)
    {
        return board.piece(position) == null;
    }

    public override bool[,] possibleMoves()
    {
        bool[,] boolMat = new bool[board.rows, board.columns];

        Position newPosition = new Position(0, 0);

        if (color == Color.White)
        {
            newPosition.defineValues(position.row - 1, position.column);
            if (board.validPosition(newPosition) && free(newPosition))
            {
                boolMat[newPosition.row, newPosition.column] = true;
            }

            newPosition.defineValues(position.row - 2, position.column);
            if (board.validPosition(newPosition) && free(newPosition) && moves == 0)
            {
                boolMat[newPosition.row, newPosition.column] = true;
            }

            newPosition.defineValues(position.row - 1, position.column - 1);
            if (board.validPosition(newPosition) && enemyExists(newPosition))
            {
                boolMat[newPosition.row, newPosition.column] = true;
            }

            newPosition.defineValues(position.row - 1, position.column + 1);
            if (board.validPosition(newPosition) && enemyExists(newPosition))
            {
                boolMat[newPosition.row, newPosition.column] = true;
            }
        }
        else
        {
            newPosition.defineValues(position.row + 1, position.column);
            if (board.validPosition(newPosition) && free(newPosition))
            {
                boolMat[newPosition.row, newPosition.column] = true;
            }

            newPosition.defineValues(position.row + 2, position.column);
            if (board.validPosition(newPosition) && free(newPosition) && moves == 0)
            {
                boolMat[newPosition.row, newPosition.column] = true;
            }

            newPosition.defineValues(position.row + 1, position.column - 1);
            if (board.validPosition(newPosition) && enemyExists(newPosition))
            {
                boolMat[newPosition.row, newPosition.column] = true;
            }

            newPosition.defineValues(position.row + 1, position.column + 1);
            if (board.validPosition(newPosition) && enemyExists(newPosition))
            {
                boolMat[newPosition.row, newPosition.column] = true;
            }
        }

        return boolMat;
    }
}
