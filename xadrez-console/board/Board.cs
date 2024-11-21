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
        pieces = new Piece[rows, columns];
    }

    public Piece piece(int rows, int columns)
    {
        return pieces[rows, columns];
    }

    public Piece piece(Position position)
    {
        return pieces[position.row, position.column];
    }

    public void placePiece(Piece piece, Position position)
    {
        if (!validPosition(position))
            throw new BoardException($"Invalid position! Position: ({position})");

        if (positionNotEmpty(position))
            throw new BoardException($"Position not empty! Position: ({position})");

        pieces[position.row, position.column] = piece;
        piece.position = position;
    }

    public Piece removePiece(Position position) 
    {
        var piecePosition = piece(position);
        if (piecePosition == null)
            return null;

        Piece aux = piecePosition;
        aux.position = null;
        pieces[position.row, position.column] = null;
        return aux;
    }

    public bool validPosition(Position position) 
    {
        return Enumerable.Range(0, rows).Contains(position.row) || Enumerable.Range(0, columns).Contains(position.column);
    }

    public bool positionNotEmpty(Position position)
    {
        return piece(position) != null;
    }
}
