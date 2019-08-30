using System;
using System.Collections.Generic;
using UnityEngine;

class M_GameManager : MonoBehaviour
{
    public GameObject boardContainer = null;
    public GameObject tileInstance = null;
    private Dictionary<Color, GameObject> atomInstances;
    private BoardManager boardManager;
    private AtomManager atomManager;
    void Start()
    {
        boardManager = new BoardManager(boardContainer, tileInstance);
        boardManager.buildBoard(8);
        initializeAtomInstances();
        atomManager = new AtomManager(ref boardManager.getBoardRef(), atomInstances, boardContainer);
        atomManager.spawnAtoms(8);
    }

    private void initializeAtomInstances()
    {
        atomInstances = new Dictionary<Color, GameObject>();
        foreach(Color c in Enum.GetValues(typeof(Color))){
            atomInstances[c] = (GameObject)Resources.Load(HelperManager.tileResourcePaths[c]);
        }
    }
}
