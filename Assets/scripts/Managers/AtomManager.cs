using System;
using System.Collections.Generic;
using UnityEngine;

class AtomManager
{
    private Board board;
    private GameObject boardContainer;
    private Dictionary<Color, GameObject> atomTypes;

    public AtomManager(ref Board b, Dictionary<Color, GameObject> atomTypes, GameObject boardContainer)
    {
        this.board = b;
        this.atomTypes = atomTypes;
        this.boardContainer = boardContainer;
    }

    public void spawnAtoms(int spawnCount)
    {
        int i = 0;
        while(i++ < spawnCount)
        {
            Color currentColor = getRandomColor();
            Tile currentTile = getRandomUnoccupiedTile();
            GameObject atomObject = GameObject.Instantiate(atomTypes[currentColor], currentTile.getGameObject().transform.position, Quaternion.identity);
            Atom newAtom = new Atom(currentTile, currentColor, atomObject);
            board.addAtom(newAtom, currentTile);
        }
    }

    private Tile getRandomUnoccupiedTile()
    {
        List<Tile> candidateTiles = board.getUnoccupiedTiles();
        return candidateTiles[UnityEngine.Random.Range(0, candidateTiles.Count)];
    }

    private Color getRandomColor()
    {
        Array colors = Enum.GetValues(typeof(Color));
        return (Color)colors.GetValue(UnityEngine.Random.Range(0, colors.Length));
    }
}
