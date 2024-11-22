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
        return position == null || piece.color != this.color;
    }

    public override bool[,] possibleMoves()
    {
        bool[,] boolMat = new bool[board.rows, board.columns];

        Position position = new Position(0, 0);

        for (int i = -1; i < 1; i++)
        {
            if (i == 0)
                continue;

            towerMoves(boolMat, position, i < 0, true, false); // up - down
            towerMoves(boolMat, position, i < 0, false, true); // right - left
        }

        return boolMat;
    }

    private void towerMoves(bool[,] boolMat, Position position, bool positive, bool rows = false, bool columns = false)
    {
        var boardRows = rows ? (positive ? board.rows + 1 : board.rows - 1) : board.rows;
        var boardColumns = columns ? (positive ? board.columns + 1 : board.columns - 1) : board.columns;

        position.defineValues(boardRows, boardColumns);
        while (board.validPosition(position) && possibleMove(position))
        {
            boolMat[position.row, position.column] = true;
            if (board.piece(position) != null && board.piece(position).color != color)
                break;

            if (rows)
                position.row = positive ? position.row + 1 : position.row - 1;

            if (columns)
                position.column = positive ? position.column + 1 : position.column - 1;
        };
    }
}
