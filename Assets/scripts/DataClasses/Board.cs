using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Board
{
    private HashSet<Atom> atoms;
    private Dictionary<Tile, GhostAtom> ghostOccupiedTiles;
    private HashSet<GhostAtom> ghostAtoms;
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
        this.ghostOccupiedTiles = new Dictionary<Tile, GhostAtom>();
        this.ghostAtoms = new HashSet<GhostAtom>();
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

    public HashSet<Tile> getUnoccupiedTiles()
    {
        return unoccupiedTiles;
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

    internal void addGhostAtom(ref GhostAtom ghostAtom)
    {
        if(!tiles.Contains(ghostAtom.getTile()))
        {
            throw new Exception("Tile to add ghost to doesnt exist here!");
        }
        ghostAtoms.Add(ghostAtom);
        ghostOccupiedTiles.Add(ghostAtom.getTile(), ghostAtom);
    }

    internal void spawnNextAtomWave()
    {
        foreach(KeyValuePair<Tile, GhostAtom> pair in ghostOccupiedTiles)
        {
            if(unoccupiedTiles.Contains(pair.Key))
            {
                Color c = pair.Value.getColor();
                GameObject atomObject = GameObject.Instantiate(
                    (GameObject)Resources.Load(ColorMethods.atomResourcePaths[c]),
                    pair.Key.getGameObject().transform);
                Atom newAtom = new Atom(pair.Key, c, atomObject);
                addAtom(newAtom, pair.Key);
            }
            ghostAtoms.Remove(pair.Value);
            GameObject.Destroy(pair.Value.GetGameObject());
        }
        ghostOccupiedTiles.Clear();
    }

    internal HashSet<Tile> getGhostOccupiedTiles()
    {
        return new HashSet<Tile>(ghostOccupiedTiles.Keys);
    }
}
