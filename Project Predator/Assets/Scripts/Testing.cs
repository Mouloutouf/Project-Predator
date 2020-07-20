using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public int amountInRow;
    public int amountInColumn;
    public int cellSize;
    public Vector2 startPosition;
    
    public List<Tile> tiles = new List<Tile>();

    private Grid grid;

    private void Start()
    {
        grid = new Grid(amountInRow, amountInColumn, cellSize, new Vector3(startPosition.x, startPosition.y), transform);
    }

    private void Update()
    {
        grid.SelectTile(Functions.GetMouseWorldPosition());

        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(Functions.GetMouseWorldPosition(), 56);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(Functions.GetMouseWorldPosition()));
        }
    }
}