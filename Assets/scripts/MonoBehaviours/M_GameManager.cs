﻿using System;
using System.Collections.Generic;
using UnityEngine;

class M_GameManager : MonoBehaviour
{
    public GameObject boardContainer = null;
    public GameObject tileInstance = null;

    private int turnNumber;
    private Dictionary<Color, GameObject> atomInstances;
    private BoardManager boardManager;
    private AtomManager atomManager;
    void Start()
    {
        this.turnNumber = 0;
        boardManager = new BoardManager(boardContainer, tileInstance);
        boardManager.buildBoard(8);
        initializeAtomInstances();
        atomManager = new AtomManager(ref boardManager.getBoardRef(), atomInstances, boardContainer);
        atomManager.spawnAtoms(8);
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if(checkForAtomHit(ray, hit))
            {
                atomManager.handleAtomSelection(hit.collider.gameObject.getParentGameObject());
            }
            else if (checkForUnoccupiedTileHit(hit) && atomManager.hasSelected())
            {
                atomManager.handleMove(hit.collider.gameObject.getParentGameObject());
            }
        }
    }

    private bool checkForAtomHit(Ray ray, RaycastHit hit)
    {
        return hit.collider.gameObject.getParentGameObject().CompareTag("atom");
    }
    private bool checkForUnoccupiedTileHit(RaycastHit hit)
    {
        return hit.collider.gameObject.getParentGameObject().CompareTag("tile");
    }

    private void initializeAtomInstances()
    {
        atomInstances = new Dictionary<Color, GameObject>();
        foreach(Color c in Enum.GetValues(typeof(Color))){
            atomInstances[c] = (GameObject)Resources.Load(HelperManager.tileResourcePaths[c]);
        }
    }
}
