using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AtomManager
{
    private Board board;
    private GameObject boardContainer;
    private Dictionary<Color, GameObject> atomTypes;
    private Dictionary<Color, GameObject> ghostAtomTypes;
    private Atom selectedAtom = null;

    public AtomManager(ref Board b, GameObject boardContainer)
    {
        this.board = b;
        this.boardContainer = boardContainer;
    }
    public void addAtomTypes(Dictionary<Color, GameObject> atomTypes)
    {
        this.atomTypes = atomTypes;
    }
    public void addGhostAtomTypes(Dictionary<Color,GameObject> ghostAtoms)
    {
        this.ghostAtomTypes = ghostAtoms;
    }
    public void spawnInitialAtoms(int spawnCount)
    {
        generateNextSpawnPoints(8);
        spawnNextAtomWave();
    }

    public bool hasSelected()
    {
        return selectedAtom != null;
    }

    //private Tile getRandomUnoccupiedTile()
    //{
    //    List<Tile> candidateTiles = board.getUnoccupiedTiles
    //    return candidateTiles[UnityEngine.Random.Range(0, candidateTiles.Count)];
    //}

    internal void handleAtomDeselection()
    {
        this.selectedAtom = null;
        Debug.Log("Atom deselected.");
    }

    public void handleAtomSelection(GameObject atom)
    {
        Atom newSelectedAtom = board.getAtom(atom);
        selectedAtom = newSelectedAtom;
        Debug.Log("Atom selected.");
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

    public void setupNextTurn()
    {
        spawnNextAtomWave();
        generateNextSpawnPoints(8);
    }

    private void generateNextSpawnPoints(int spawnCount)
    {
        //int i = 0;
        //while (i++ < spawnCount)
        //{
        //    Color currentColor = getRandomColor();
        //    Tile currentTile = getRandomUnoccupiedTile();
        //    GameObject atomObject = GameObject.Instantiate(
        //        ghostAtomTypes[currentColor], currentTile.getGameObject().transform);
        //    GhostAtom newGhostAtom = new GhostAtom(currentTile, currentColor, atomObject);
        //    board.addGhostAtom(ref newGhostAtom);
        //}
        
        HashSet<Tile> candidateSet = getSpawnPointsCandidates();
        List<Tile> spawnPointsList = new List<Tile>(candidateSet);
        System.Random randomizer = new System.Random();
        for(int i = 0; i < spawnCount; i++)
        {
            int randomIndex = randomizer.Next(spawnPointsList.Count);
            Color currentColor = getRandomColor();
            Tile currentTile = spawnPointsList[randomIndex];
            spawnPointsList.Remove(currentTile);

            GameObject atomObject = GameObject.Instantiate(
                ghostAtomTypes[currentColor], currentTile.getGameObject().transform);
            GhostAtom newGhostAtom = new GhostAtom(currentTile, currentColor, atomObject);
            board.addGhostAtom(ref newGhostAtom);
        }
    }

    private HashSet<Tile> getSpawnPointsCandidates()
    {
        ArrayList result = new ArrayList();
        HashSet<Tile> unoccupiedTiles = board.getUnoccupiedTiles();
        HashSet<Tile> ghostOccupiedTiles = board.getGhostOccupiedTiles();
        unoccupiedTiles.ExceptWith(ghostOccupiedTiles);
        return unoccupiedTiles;
    }

    private void spawnNextAtomWave()
    {
        board.spawnNextAtomWave();
    }
}
