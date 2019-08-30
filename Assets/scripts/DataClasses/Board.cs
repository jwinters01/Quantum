using System.Collections.Generic;

class Board
{
    private HashSet<Atom> atoms;
    private Tile[,] tileMap;
    private HashSet<Tile> tiles;

    public Board(Tile[,] tileMap)
    {
        this.tileMap = tileMap;
        this.tiles = new HashSet<Tile>();
        fillTiles();
        this.atoms = new HashSet<Atom>();
    }
    public bool addAtom(Tile location, Color color)
    {
        if (tiles.Contains(location) && location.isEmpty())
        {
            Atom newAtom = new Atom(location, color);
            location.setAtom(ref newAtom);
            atoms.Add(newAtom);
            //
        }
        return false;
    }


    private void fillTiles()
    {
        for(int i = 0; i < tileMap.GetLength(0); i++)
        {
            for(int j = 0; j < tileMap.GetLength(1); j++)
            {
                tiles.Add(tileMap[i, j]);
            }
        }
    }
}
