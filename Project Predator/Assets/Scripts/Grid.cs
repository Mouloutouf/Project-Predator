using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid
{
    private int width;
    private int height;

    private int cellSize;

    private Vector3 originPosition;
    
    private Tile[,] tileArray;

    /// Instantiate Class : Initialize Grid

    public Grid(int width, int height, int cellSize, Vector3 originPosition, Transform parent)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        
        tileArray = new Tile[width, height];

        for (int x = 0; x < tileArray.GetLength(0); x++)
        {
            for (int y = 0; y < tileArray.GetLength(1); y++)
            {
                GetTiles(parent, x, y);
            }
        }

        //DrawGrid();
    }
    
    private void GetTiles(Transform parent, int x, int y)
    {
        Vector3 tilePos;
        ConvertCoordinatesToLocalPosition(x, y, out tilePos);
        
        foreach (Transform child in parent)
        {
            if (child.localPosition == tilePos)
            {
                tileArray[x, y] = child.gameObject.GetComponent<Tile>();
            }
        }
    }
    
    //\

    /// Conversion Methods : World to Grid and vice versa

    private void ConvertCoordinatesToLocalPosition(int x, int y, out Vector3 localPosition)
    {
        int _x = (x * cellSize) + (cellSize / 2);
        int _y = (y * cellSize) + (cellSize / 2);
        localPosition = new Vector3(_x, _y);
    }

    private void ConvertCoordinatesToWorldPosition(int x, int y, out Vector3 worldPosition)
    {
        ConvertCoordinatesToLocalPosition(x, y, out worldPosition);

        worldPosition += originPosition;
    }
    
    private void ConvertPositionToGridCoordinates(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    //\

    /// Grid Visual : Gizmos Drawing Method

    private void DrawGrid()
    {
        for (int x = 0; x < tileArray.GetLength(0); x++)
        {
            for (int y = 0; y < tileArray.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    //\

    /// Core Methods : Hover, Selection, Information

    private int currentX;
    private int currentY;

    private Tile selected;

    public void HoverTile(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (x == currentX && y == currentY) return;

            foreach (Tile tile in tileArray) if (tile != selected) tile.hoverObject.SetActive(false);
            tileArray[x, y].hoverObject.SetActive(true);

            currentX = x;
            currentY = y;
        }
    }

    public void HoverTile(Vector3 worldPosition)
    {
        int x, y;
        ConvertPositionToGridCoordinates(worldPosition, out x, out y);
        HoverTile(x, y);
    }

    public void SelectTile(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            foreach (Tile tile in tileArray) { tile.hoverObject.SetActive(false); tile.hoverObject.GetComponent<SpriteRenderer>().color = Color.white; }
            tileArray[x, y].hoverObject.SetActive(true);
            tileArray[x, y].hoverObject.GetComponent<SpriteRenderer>().color = Color.yellow;

            selected = tileArray[x, y];
        }
    }

    public void SelectTile(Vector3 worldPosition)
    {
        int x, y;
        ConvertPositionToGridCoordinates(worldPosition, out x, out y);
        SelectTile(x, y);
    }

    //\
}