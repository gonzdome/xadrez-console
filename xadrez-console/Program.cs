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
            Screen.printBoard(newMatch.board);

        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
