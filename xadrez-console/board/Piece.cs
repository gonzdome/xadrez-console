using board;

namespace board;

public class Piece
{
    public Position position { get; set; }
    public Color color { get; protected set; }
    public int moves { get; protected set; }
    public Board board {  get; protected set; }

    public Piece(Position position, Color color, int moves, Board board)
    {
        this.position = position;
        this.color = color;
        this.moves = 0;
        this.board = board;
    }
}
