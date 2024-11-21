﻿using board;
using board.pieces;
using screen;

namespace xadrez_console;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Board board = new Board(8, 8);
            
            board.placePiece(new Tower(board, Color.White), new Position(0, 0));
            board.placePiece(new Tower(board, Color.White), new Position(0, 7));
            board.placePiece(new Tower(board, Color.Yellow), new Position(7, 0));
            board.placePiece(new Tower(board, Color.Yellow), new Position(7, 7));

            var boardPosition = new BoardPosition("a", board.rows);
            Screen.printBoard(board);
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
