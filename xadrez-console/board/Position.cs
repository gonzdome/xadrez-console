﻿namespace board;

public class Position
{
    public int row { get; set; }
    public int column { get; set; }

    public Position(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public void defineValues(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public override string ToString()
    {
        return $"{row}, {column}";
    }
}
