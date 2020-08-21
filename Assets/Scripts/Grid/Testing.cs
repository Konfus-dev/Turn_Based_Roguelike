using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Grid grid;
    [SerializeField]
    public GameObject tileSelect;
    [SerializeField]
    public Vector2Int gridSize;

    public void Start()
    {
        grid = new Grid(gridSize.x, gridSize.y, 1f, new Vector3(0, 0), tileSelect, this.transform);
        if (Player.Instance.gameObject != null)
        {
            Node n = grid.GetNode(1, 1);
            PlayerMovement pm = Player.Instance.gameObject.GetComponent<PlayerMovement>();
            if (pm.myNode == null && n != null)
            {
                pm.MoveTo(n);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (grid != null)
            {
                Vector3 pos = Player.Instance.transform.position;
                if (grid.ValueExists(pos)) {
                    grid.SetValue(pos, grid.GetValue(pos) + "1");
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (grid != null)
            {
                Vector3 pos = Player.Instance.transform.position;
                if (grid.ValueExists(pos))
                {
                    Debug.Log(grid.GetValue(pos));
                }
            }
        }
    }
}
