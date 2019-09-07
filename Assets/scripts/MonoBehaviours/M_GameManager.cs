using System;
using System.Collections.Generic;
using UnityEngine;

public class M_GameManager : MonoBehaviour
{
    public GameObject boardContainer = null;
    public GameObject tileInstance = null;

    private int turnNumber;
    private Dictionary<Color, GameObject> atomInstances;
    private Dictionary<Color, GameObject> ghostAtomInstances;
    private BoardManager boardManager;
    private AtomManager atomManager;
    void Start()
    {
        this.turnNumber = 0;
        boardManager = new BoardManager(boardContainer, tileInstance);
        boardManager.buildBoard(8);
        initializeAtomInstances();
        atomManager = new AtomManager(ref boardManager.getBoardRef(), boardContainer);
        atomManager.addAtomTypes(atomInstances);
        atomManager.addGhostAtomTypes(ghostAtomInstances);
        atomManager.spawnInitialAtoms(8);
        atomManager.setupNextTurn();
    }

    void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                handleHit(hit);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            atomManager.handleAtomDeselection();
        }
    }

    private void handleHit(RaycastHit hit)
    {
        if (checkForAtomHit(hit))
        {
            atomManager.handleAtomSelection(hit.collider.gameObject.getParentGameObject());
        }
        else if (checkForUnoccupiedTileHit(hit) && atomManager.hasSelected())
        {
            makeTurn(hit);
        }
    }
    private bool checkForAtomHit(RaycastHit hit)
    {
        return hit.collider.gameObject.getParentGameObject().CompareTag("atom");
    }
    private bool checkForUnoccupiedTileHit(RaycastHit hit)
    {
        return hit.collider.gameObject.getParentGameObject().CompareTag("tile");
    }
    private void makeTurn(RaycastHit hit)
    {
        atomManager.handleMove(hit.collider.gameObject.getParentGameObject());
        Debug.Log($"Turn {turnNumber} complete.");
        setupNextTurn();
    }
    private void setupNextTurn()
    {
        atomManager.setupNextTurn();
        turnNumber++;
    }
    private void initializeAtomInstances()
    {
        atomInstances = new Dictionary<Color, GameObject>();
        ghostAtomInstances = new Dictionary<Color, GameObject>();
        foreach(Color c in Enum.GetValues(typeof(Color))){
            atomInstances[c] = (GameObject)Resources.Load(ColorMethods.atomResourcePaths[c]);
            ghostAtomInstances[c] = (GameObject)Resources.Load(ColorMethods.ghostAtomResourcePaths[c]);
        }
    }
}
