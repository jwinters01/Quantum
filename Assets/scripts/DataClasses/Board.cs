using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Board
{
    private HashSet<Atom> atoms;
    private Tile[,] tileMap;
    private HashSet<Tile> tiles;
    private HashSet<Tile> unoccupiedTiles;
    private Dictionary<GameObject, Atom> atomObjectMap;
    private Dictionary<GameObject, Tile> tileObjectMap;

    public Board(Tile[,] tileMap)
    {
        this.atomObjectMap = new Dictionary<GameObject, Atom>();
        this.tileObjectMap = new Dictionary<GameObject, Tile>();
        this.tileMap = tileMap;
        this.tiles = new HashSet<Tile>();
        this.unoccupiedTiles = new HashSet<Tile>();
        fillTiles();
        this.atoms = new HashSet<Atom>();
    }

    public void addAtom(Atom newAtom, Tile tile)
    {
        if (!tiles.Contains(tile))
        {
            throw new Exception("Adding atom to tile that doesn't exist on board!");
        }
        tile.setAtom(ref newAtom);
        atoms.Add(newAtom);
        unoccupiedTiles.Remove(tile);
        atomObjectMap.Add(newAtom.GetGameObject(), newAtom);
    }

    public List<Tile> getUnoccupiedTiles()
    {
        return unoccupiedTiles.ToList();
    }

    public Atom removeAtom(Tile location)
    {
        if (unoccupiedTiles.Contains(location))
        {
            return null;
        }
        Atom removedAtom = location.removeAtom();
        atoms.Remove(removedAtom);
        unoccupiedTiles.Add(location);
        return removedAtom;
    }

    public Tile getTile(GameObject tile)
    {
        return tileObjectMap[tile];
    }

    public Atom getAtom(GameObject atom)
    {
        return atomObjectMap[atom];
    }

    private void fillTiles()
    {
        for(int i = 0; i < tileMap.GetLength(0); i++)
        {
            for(int j = 0; j < tileMap.GetLength(1); j++)
            {
                tiles.Add(tileMap[i, j]);
                unoccupiedTiles.Add(tileMap[i, j]);
                tileObjectMap.Add(tileMap[i, j].getGameObject(), tileMap[i, j]);
            }
        }
    }
}
