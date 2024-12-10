using board;
using game;
using screen;

namespace xadrez_console;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Match match = new();

            while (!match.finished)
            {
                try
                {
                    Console.Clear();
                    Screen.printMatch(match);

                    Console.Write("Origin: ");
                    var origin = Screen.readBoardPosition().toPosition();
                    match.validateOriginPosition(origin);

                    // Show the possible path/steps for the piece
                    bool[,] possiblePositions = match.board.piece(origin).possibleMoves();
                    Console.Clear();
                    Screen.printBoard(match.board, possiblePositions);

                    Console.Write("Destiny: ");
                    var destiny = Screen.readBoardPosition().toPosition();

                    match.validateDestinyPosition(origin, destiny);
                    match.executePlay(origin, destiny);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
