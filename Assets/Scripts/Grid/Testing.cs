using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private Grid grid;

    private void Start()
    {
        grid = new Grid(4, 2, 1f, new Vector3(20, 0));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(UtilsClass.GetMouseWorldPosition().x + " " + UtilsClass.GetMouseWorldPosition().y + " " + UtilsClass.GetMouseWorldPosition().z);
            if (grid != null)
            {
                Vector3 pos = UtilsClass.GetMouseWorldPosition();
                if (grid.ValueExists(pos)) {
                    grid.SetValue(pos, grid.GetValue(pos) + 1);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (grid != null)
            {
                Vector3 pos = UtilsClass.GetMouseWorldPosition();
                if (grid.ValueExists(pos))
                {
                    //grid.SetValue(UtilsClass.GetMouseWorldPosition(), 17);
                    Debug.Log(grid.GetValue(pos));
                }
            }
        }
    }
}
