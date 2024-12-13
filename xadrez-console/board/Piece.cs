namespace board;

abstract class Piece
{
    public Position? position { get; set; }
    public Color color { get; protected set; }
    public int moves { get; protected set; }
    public Board board {  get; protected set; }

    public Piece(Board board, Color color)
    {
        this.position = null;
        this.board = board;
        this.color = color;
        this.moves = 0;
    }

    public void addMoveQuantity()
    {
        moves++;
    }

    public void removeMoveQuantity()
    {
        moves--;
    }

    public bool hasPossibleMoves()
    {
        bool[,] mat = possibleMoves();
        for (int i = 0; i < board.rows; i++)
        {
            for (int j = 0; j < board.columns; j++)
            {
                if(mat[i, j])
                    return true;
            }
        }
        return false;
    }

    public bool canMoveTo(Position position)
    {
        return possibleMoves()[position.row, position.column];
    }

    public abstract bool[,] possibleMoves();
}
