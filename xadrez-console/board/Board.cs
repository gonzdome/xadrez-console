namespace board;

public class Board
{
    public int rows {  get; set; }
    public int columns { get; set; }
    private Piece[,] pieces;

    public Board(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        this.pieces = new Piece[rows, columns];
    }

    public Piece piece (int rows, int columns)
    {
        return pieces[rows, columns];
    }
}
