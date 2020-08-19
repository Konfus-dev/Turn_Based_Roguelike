using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private Node[,] nodeArray;
    private TextMesh[,] debugTextArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, GameObject nodeTile, Transform t)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        nodeArray = new Node[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < nodeArray.GetLength(0); x++)
        {
            for (int y = 0; y < nodeArray.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                Node n = GameObject.Instantiate(nodeTile, GetWorldPosition(x, y) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity, t).GetComponent<Node>();
                n.gameObject.name = x + "," + y;
                n.name = x + "," + y;
                nodeArray[x, y] = n;
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        for (int x = 0; x < nodeArray.GetLength(0); x++)
        {
            for (int y = 0; y < nodeArray.GetLength(1); y++)
            {
                if (x > 0)
                {
                    nodeArray[x, y].left = nodeArray[x - 1, y];
                }
                else if (x < nodeArray.GetLength(0) - 1)
                {
                    nodeArray[x, y].right = nodeArray[x + 1, y];
                }
                else if (y > 0)
                {
                    nodeArray[x, y].down = nodeArray[x, y - 1];
                }
                else if (y < nodeArray.GetLength(1) - 1)
                {
                    nodeArray[x, y].up = nodeArray[x, y + 1];
                }
            }
        }
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

    public void SetValue(int x, int y, string value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            nodeArray[x, y].name = value;
            Debug.Log(nodeArray[x, y].name);
        }
    }

    public void SetValue(Vector3 worldPosition, string value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public bool ValueExists(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < width && y < height);
    }

    public bool ValueExists(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return ValueExists(x, y);
    }

    public string GetValue(int x, int y)
    {
        if (ValueExists(x, y))
        {
            return nodeArray[x, y].name;
        }
        else
        {
            return "";
        }
    }

    public string GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public Node GetNode(int x, int y)
    {
        if (ValueExists(x, y))
        {
            return nodeArray[x, y];
        }
        return null;
    }

}
