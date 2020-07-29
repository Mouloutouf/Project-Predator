using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public int amountInRow;
    public int amountInColumn;
    public int cellSize;
    public Vector2 startPosition;
    
    private Grid grid;

    private delegate void Click(Vector3 position);
    private Click clickDelegate;

    private void Start()
    {
        grid = new Grid(amountInRow, amountInColumn, cellSize, new Vector3(startPosition.x, startPosition.y), transform);

        clickDelegate = grid.SelectTile;
    }

    private void Update()
    {
        grid.HoverTile(Functions.GetMouseWorldPosition());

        if (Input.GetMouseButtonDown(0))
        {
            clickDelegate(Functions.GetMouseWorldPosition());
        }

        if (Input.GetMouseButtonDown(1))
        {
            //grid.GetInfoOnTile();
        }
    }
}