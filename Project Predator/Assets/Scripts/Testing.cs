using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public int amountInRow;
    public int amountInColumn;
    public float cellSize;
    public Vector2 startPosition;
    
    public List<Case> _cases = new List<Case>();

    private Grid grid;

    public GameObject casePrefab;

    private void Start()
    {
        grid = new Grid(amountInRow, amountInColumn, cellSize, new Vector3(startPosition.x, startPosition.y), transform, casePrefab);
    }

    private void Update()
    {
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