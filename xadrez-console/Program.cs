﻿using tabuleiro;
using tela;

namespace xadrez_console;

class Program
{
    static void Main(string[] args)
    {
        Tabuleiro tabuleiro = new Tabuleiro(8, 8);

        Tela.imprimirTabuleiro(tabuleiro);
    }
}
