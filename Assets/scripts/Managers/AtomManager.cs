﻿using System;
using System.Collections.Generic;
using UnityEngine;

class AtomManager
{
    private Board board;
    private GameObject boardContainer;
    private Dictionary<Color, GameObject> atomTypes;
    private Atom selectedAtom = null;

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
            GameObject atomObject = GameObject.Instantiate(atomTypes[currentColor], currentTile.getGameObject().transform);
            Atom newAtom = new Atom(currentTile, currentColor, atomObject);
            board.addAtom(newAtom, currentTile);
        }
    }

    public bool hasSelected()
    {
        return selectedAtom != null;
    }

    private Tile getRandomUnoccupiedTile()
    {
        List<Tile> candidateTiles = board.getUnoccupiedTiles();
        return candidateTiles[UnityEngine.Random.Range(0, candidateTiles.Count)];
    }

    public void handleAtomSelection(GameObject atom)
    {
        Atom newSelectedAtom = board.getAtom(atom);
        if (selectedAtom != null && selectedAtom.Equals(newSelectedAtom))
        {
            selectedAtom = null;
            Debug.Log("Atom deselected.");
        }
        else
        {
            selectedAtom = newSelectedAtom;
            Debug.Log("Atom selected.");
        }
    }

    public void handleMove(GameObject tile)
    {
        Tile t = board.getTile(tile);
        if (this.hasSelected())
        {
            selectedAtom.getTile().removeAtom();
            moveAtom(selectedAtom, tile);
            t.acceptAtom(selectedAtom);
            selectedAtom = null;
            Debug.Log("Move made. selection cleared.");
        }
    }

    private void moveAtom(Atom a, GameObject t)
    {

    }

    private Color getRandomColor()
    {
        Array colors = Enum.GetValues(typeof(Color));
        return (Color)colors.GetValue(UnityEngine.Random.Range(0, colors.Length));
    }
}
