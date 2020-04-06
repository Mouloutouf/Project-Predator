using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid
{
    private int width;
    private int height;

    private float cellSize;

    private Vector3 originPosition;

    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    private GameObject[,] caseArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Transform parent, GameObject prefab)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];
        caseArray = new GameObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                caseArray[x, y] = CreateCase(prefab, parent, GetLocalPosition(x, y) + new Vector3(cellSize, cellSize) * .5f);
                debugTextArray[x, y] = caseArray[x, y].GetComponent<TextMesh>();
                    //Functions.CreateWorldText(gridArray[x, y].ToString(), parent, GetLocalPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private GameObject CreateCase(GameObject _prefab, Transform parent, Vector3 localPosition)
    {
        GameObject caseObject = GameObject.Instantiate(_prefab);
        caseObject.transform.SetParent(parent, false);
        caseObject.transform.localPosition = localPosition;
        caseObject.GetComponent<MeshRenderer>().sortingOrder = 5000;

        return caseObject;
    }

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

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
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
            return gridArray[x, y];
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
}