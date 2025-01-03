using game;

namespace board.pieces;

class Pawn : Piece
{
    private Match match;
    public Pawn(Board board, Color color, Match match) : base(board, color)
    {
        this.match = match;
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

            // En Passant
            if (position.row == 3)
            {
                Position left = new Position(position.row, position.column - 1);
                if(board.validPosition(left) && enemyExists(left) && board.piece(left) == match.vulnerableEnPassant)
                    boolMat[left.row - 1, left.column] = true;

                Position right = new Position(position.row, position.column + 1);
                if (board.validPosition(right) && enemyExists(right) && board.piece(right) == match.vulnerableEnPassant)
                    boolMat[right.row - 1, right.column] = true;
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

            // En Passant
            if (position.row == 4)
            {
                Position left = new Position(position.row, position.column - 1);
                if (board.validPosition(left) && enemyExists(left) && board.piece(left) == match.vulnerableEnPassant)
                    boolMat[left.row + 1, left.column] = true;

                Position right = new Position(position.row, position.column + 1);
                if (board.validPosition(right) && enemyExists(right) && board.piece(right) == match.vulnerableEnPassant)
                    boolMat[right.row + 1, right.column] = true;
            }
        }
        
        return boolMat;
    }
}