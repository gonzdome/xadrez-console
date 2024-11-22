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
        return position == null || piece.color != this.color;
    }

    public override bool[,] possibleMoves()
    {
        bool[,] boolMat = new bool[board.rows, board.columns];

        Position position = new Position(0, 0);

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;

                var valueDefinedForRow = i < 0 ? position.row - i : position.row + i;
                var valueDefinedForColumn = j < 0 ? position.column - j : position.column + j;
                position.defineValues(valueDefinedForRow, valueDefinedForColumn);

                if (board.validPosition(position) && possibleMove(position))
                    boolMat[position.row, position.column] = true;
            }
        }

        return boolMat;
    }
}
