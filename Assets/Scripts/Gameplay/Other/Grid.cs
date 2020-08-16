
using System;
using UnityEngine;

public class Grid
{

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int Width;
    private int Height;
    private float CellSize;
    private Vector3 OriginPosition;
    private int[,] GridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.Width = width;
        this.Height = height;
        this.CellSize = cellSize;
        this.OriginPosition = originPosition;

        GridArray = new int[width, height];

        bool showDebug = true;
        if (showDebug)
        {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < GridArray.GetLength(0); x++)
            {
                for (int y = 0; y < GridArray.GetLength(1); y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = GridArray[eventArgs.x, eventArgs.y].ToString();
            };
        }
    }

    public int GetWidth()
    {
        return Width;
    }

    public int GetHeight()
    {
        return Height;
    }

    public float GetCellSize()
    {
        return CellSize;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * CellSize + OriginPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - OriginPosition).x / CellSize);
        y = Mathf.FloorToInt((worldPosition - OriginPosition).y / CellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < Width && y < Height)
        {
            GridArray[x, y] = value;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
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
        if (x >= 0 && y >= 0 && x < Width && y < Height)
        {
            return GridArray[x, y];
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
