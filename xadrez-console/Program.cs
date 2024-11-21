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
            Match newMatch = new ();          

            while(!newMatch.finished)
            {
                Console.Clear();
                Screen.printBoard(newMatch.board);

                Console.Write("Origin:");
                var origin = Screen.readBoardPosition().toPosition();

                Console.Write("Destiny:");
                var destiny = Screen.readBoardPosition().toPosition();

                newMatch.makeNewMove(origin, destiny);
            }
        }
        catch (BoardException e)
        { 
            Console.WriteLine(e.Message);
        }
    }
}
