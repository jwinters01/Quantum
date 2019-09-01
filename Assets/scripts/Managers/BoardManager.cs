using System;
using System.Collections.Generic;
using UnityEngine;


class BoardManager
{
    private GameObject tileInstance;
    public GameObject tilesContainer;
    private Board board;

    public int boardSize;
    private Tile[,] tiles;
    private Bounds tileBounds;


    public BoardManager(GameObject tilesContainer, GameObject tileInstance)
    {
        this.tilesContainer = tilesContainer;
        this.tileInstance = tileInstance;
        this.tileBounds = tileInstance.FindComponentInChildWithTag<MeshRenderer>("Visual").bounds;
    }
    
    public void buildBoard(int boardSize)
    {
        this.boardSize = boardSize;
        this.tiles = new Tile[boardSize, boardSize];
        Vector3 startPoint = getBoardStartPoint();
        for(int i = 0; i < boardSize; i++)
        {
            buildRow(i, getRowStartPoint(i, startPoint));
        }
        for(int i = 0; i < boardSize; i++)
        {
            calculateNeighborsForRow(i);
        }
        this.board = new Board(tiles);
    }

    public ref Board getBoardRef()
    {
        return ref this.board;
    }

    private void buildRow(int rowNumber, Vector3 rowStartPoint)
    {
        for(int i = 0; i < boardSize; i++)
        {
            Vector3 currentLocation = new Vector3(
                rowStartPoint.x + i * tileBounds.size.x,
                rowStartPoint.y,
                rowStartPoint.z * tileBounds.size.z );
            GameObject obj = GameObject.Instantiate(tileInstance, currentLocation, Quaternion.identity, tilesContainer.transform);
            Tile t = new Tile(ref obj);
            tiles[rowNumber, i] = t;
        }
    }
    private void calculateNeighborsForRow(int row)
    {
        for(int col=0; col < boardSize; col++)
        {
            Tile currentTile = tiles[row, col];
            Dictionary<Direction, Tile> currentTileNeighbors = new Dictionary<Direction, Tile>();
            foreach(Direction d in Enum.GetValues(typeof(Direction)))
            {
                int[] indeces = DirectionMethods.getNeighborIndex(row, col, d);
                currentTileNeighbors[d] = areValid(indeces) ?
                    tiles[indeces[0], indeces[1]] : null;
            }
            currentTile.setNeighbors(currentTileNeighbors);
        }
    }
    public bool areValid(int[] indeces)
    {
        return indeces[0] >= 0 && indeces[0] < boardSize
            && indeces[1] >= 0 && indeces[1] < boardSize;
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
