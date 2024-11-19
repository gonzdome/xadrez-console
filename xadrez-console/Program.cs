using tabuleiro;

namespace xadrez_console;

class Program
{
    static void Main(string[] args)
    {
        Posicao posicao = new Posicao(3, 4);
        Tabuleiro tabuleiro = new Tabuleiro(8, 8);

        Console.WriteLine(posicao);
    }
}
