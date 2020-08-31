﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void _Action_(int x, int y);

public class Grid : MonoBehaviour
{
    public int width;
    public int height;

    public int cellSize;

    public Vector3 originPosition;
    
    private Tile[,] tileArray;
    public Tile[,] TileArray { get => tileArray; set => tileArray = value; }

    public Transform player;

    private _Action_ clickAction_;

    //\

    public int moveArea;

    private void Start()
    {
        CreateGrid();

        clickAction_ = SelectTile; //Replace with Reset
    }

    private void Update()
    {
        Action(Functions.GetMouseWorldPosition(), HoverTile);

        if (Input.GetMouseButtonDown(0))
        {
            if (clickAction_ != null) Action(Functions.GetMouseWorldPosition(), clickAction_);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //grid.GetInfoOnTile();
        }
    }
    
    public void Reset()
    {
        clickAction_ = null;
    }

    /// Initialize Grid : Get Tiles into Table Array

    public void CreateGrid()
    {
        tileArray = new Tile[width, height];

        for (int x = 0; x < tileArray.GetLength(0); x++)
        {
            for (int y = 0; y < tileArray.GetLength(1); y++)
            {
                GetTile(transform, x, y);
            }
        }
    }
    
    private void GetTile(Transform parent, int x, int y)
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

    public void Action(Vector3 worldPosition, _Action_ action_)
    {
        int x, y;
        ConvertPositionToGridCoordinates(worldPosition, out x, out y);

        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            action_(x, y);
        }
    }

    private int currentX;
    private int currentY;

    private Tile selected;

    public void HoverTile(int x, int y)
    {
        if (x == currentX && y == currentY) return;

        foreach (Tile tile in tileArray) if (tile != selected) tile.hoverObject.SetActive(false);
        tileArray[x, y].hoverObject.SetActive(true);

        currentX = x;
        currentY = y;
    }

    public void SelectTile(int x, int y)
    {
        foreach (Tile tile in tileArray) { tile.hoverObject.SetActive(false); tile.hoverObject.GetComponent<SpriteRenderer>().color = Color.white; }
        tileArray[x, y].hoverObject.SetActive(true);
        tileArray[x, y].hoverObject.GetComponent<SpriteRenderer>().color = Color.yellow;

        selected = tileArray[x, y];
    }
    
    public void MoveToTile(int x, int y)
    {
        player.position = tileArray[x, y].gameObject.transform.position;
    }

    public void AttackTile(int x, int y)
    {
        if (tileArray[x, y].enemy != null)
        {
            // Kill the Enemy
        }
    }

    //\

    public void SetTilesToAction(Vector3 playerPosition, int area)
    {
        int x, y;
        ConvertPositionToGridCoordinates(playerPosition, out x, out y);

        int minX, minY, maxX, maxY;
        minX = Mathf.Clamp(x - area, 0, width);
        maxX = Mathf.Clamp(x + area, 0, width);
        minY = Mathf.Clamp(y - area, 0, height);
        maxY = Mathf.Clamp(y + area, 0, height);

        for (int _x = 0; _x < width; _x++)
        {
            for (int _y = 0; _y < height; _y++)
            {
                tileArray[_x, _y].inActionArea = (_x >= minX && _y >= minY && _x < maxX && _y < maxY) ? true : false;
            }
        }
    }
}