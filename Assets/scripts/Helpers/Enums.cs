using System;
using System.Collections.Generic;

public enum Direction
{
    N, NE, E, SE, S, SW, W, NW
};
public static class DirectionMethods
{
    public static int[] getNeighborIndex( int row, int col, Direction d)
    {
        switch (d)
        {
            case Direction.N:
                return new int[2] { row-1, col };
            case Direction.NE:
                return new int[2] { row-1, col+1 };
            case Direction.E:
                return new int[2] { row, col+1 };
            case Direction.SE:
                return new int[2] { row+1, col+1 };
            case Direction.S:
                return new int[2] { row+1, col };
            case Direction.SW:
                return new int[2] { row+1, col-1 };
            case Direction.W:
                return new int[2] { row, col-1 };
            case Direction.NW:
                return new int[2] { row-1, col-1 };
            default:
                throw new Exception("Invalid Direction(?)");
        }

    }
}
public enum Color
{
    BLUE, GREEN, PURPLE, RED
}
public static class ColorMethods
{
    public static string atomBasePath = "Prefabs/atoms/";
    public static Dictionary<Color, string> atomResourcePaths = new Dictionary<Color, string>()
    {
        {Color.BLUE, atomBasePath + "Atom_blue" },
        {Color.GREEN, atomBasePath + "Atom_green" },
        {Color.PURPLE, atomBasePath + "Atom_purple" },
        {Color.RED, atomBasePath + "Atom_red" },
    };
}