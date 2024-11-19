namespace board;

public class BoardPosition
{
    public int row { get; set; }
    public string column { get; set; }

    public BoardPosition(string column, int row)
    {
        this.row = row;
        this.column = column;
    }

    public Position toPosition()
    {
        return new Position(8 - row, 'A' - char.Parse(column.ToUpper()));
    }

    public override string ToString()
    {
        return $"{column}{row}";
    }
}
