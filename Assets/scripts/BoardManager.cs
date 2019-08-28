using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class BoardManager : MonoBehaviour
{
    public GameObject tileInstance;
    public GameObject tilesContainer;
    public int boardSize;
    private GameObject[,] tiles;
    private Bounds tileBounds;

    void Start()
    {
        tileBounds = tileInstance.FindComponentInChildWithTag<MeshRenderer>("Visual").bounds;
        Debug.Log($"Tile Bounds = {tileBounds}");
        buildBoard();
    }
    
    void Update()
    {

    }

    // Helpers
    private void buildBoard()
    {
        Vector3 startPoint = getBoardStartPoint();
        for(int i = 0; i < boardSize; i++)
        {
            buildRow(i, getRowStartPoint(i, startPoint));
        }
    }
    private void buildRow(int rowNumber, Vector3 rowStartPoint)
    {
        Debug.Log($"Row start point = {rowStartPoint}.");
        for(int i = 0; i < boardSize; i++)
        {
            Vector3 currentLocation = new Vector3(
                rowStartPoint.x + i * tileBounds.size.x,
                rowStartPoint.y,
                rowStartPoint.z * tileBounds.size.z );
            GameObject obj = GameObject.Instantiate(tileInstance, currentLocation, Quaternion.identity, tilesContainer.transform);
            //TODO: create Tile class for maining Tile state and storing GameObject
        }
    }
    private Vector3 getBoardStartPoint()
    {
        float xPos = -(tileBounds.size.x * (float)boardSize / 2) + tileBounds.size.x/2;
        float zPos = (tileBounds.size.z * (float)boardSize / 2) - tileBounds.size.z/2;
        return new Vector3(
            xPos,
            0,
            zPos);
    }
    private Vector3 getRowStartPoint(int rowNumber, Vector3 boardStartPoint)
    {
        return new Vector3(
            boardStartPoint.x,
            boardStartPoint.y,
            boardStartPoint.z - rowNumber*tileBounds.size.z);
    }
}
