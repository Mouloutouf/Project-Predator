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
                GetTile(parent, x, y);

                // caseArray[x, y].selectable.SetActive(false);
                // caseArray[x, y].notSelectable.SetActive(false);
            }
        }

        DrawGrid();
    }

    #region Initialize Tiles
    private void GetTile(Transform parent, int x, int y)
    {
        int posX = Convert(x);
        int posY = Convert(y);
        Vector3 tilePos = new Vector3(posX, posY);

        foreach (Transform child in parent)
        {
            if (child.position == tilePos)
            {
                tileArray[x, y] = child.gameObject.GetComponent<Tile>();
            }
        }
    }

    private int Convert(int value)
    {
        return (value * cellSize) + (cellSize / 2);
    }

    private Tile CreateTile(GameObject _prefab, Transform parent, Vector3 localPosition)
    {
        GameObject _tileObject = GameObject.Instantiate(_prefab);
        _tileObject.transform.SetParent(parent, false);
        _tileObject.transform.localPosition = localPosition;
        _tileObject.GetComponent<MeshRenderer>().sortingOrder = 5000;

        Tile _tile = _tileObject.GetComponent<Tile>();

        return _tile;
    }
    #endregion

    #region Convert Position
    private Vector3 GetLocalPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    #endregion

    #region Get Set Value
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            // gridArray[x, y] = value;
            // debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            // return gridArray[x, y];
            return 1; // Required
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
    #endregion

    #region Utilities
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
    #endregion

    private int currentX;
    private int currentY;

    public void SelectTile(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (x == currentX && y == currentY) return;

            foreach (Tile tile in tileArray)
            {
                tile.selectable.SetActive(false);
                tile.notSelectable.SetActive(false);
            }

            if (tileArray[x, y].selectionState) tileArray[x, y].selectable.SetActive(true);
            else tileArray[x, y].notSelectable.SetActive(true);

            currentX = x;
            currentY = y;
        }

        else return;
    }

    public void SelectTile(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SelectTile(x, y);
    }
}