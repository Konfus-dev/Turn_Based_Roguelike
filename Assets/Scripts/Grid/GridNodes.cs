using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNodes : MonoBehaviour
{
    bool checkItems = true;
    public Node[] nodeList;
    public Vector3[] locations;
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (checkItems)
        {
            checkItems = false;
            MakeChildrenUseful();
            
        }
    }

    void MakeChildrenUseful()
    {
        nodeList = new Node[transform.childCount];
        locations = new Vector3[transform.childCount];
        int children = transform.childCount;
        for(int i = 0; i < children; i++)
        {
            Transform child = transform.GetChild(i);
            Node n = child.GetComponent<Node>();
            if (n != null)
            {
                nodeList[i] = n;
                locations[i] = child.localPosition;
                if (n.name.Equals("SpawnPoint"))
                {
                    Debug.Log("o no");
                    player.Spawn(n);
                }
            }
        }

        for (int i = 0; i < children; i++)
        {
            float x = locations[i].x;
            float y = locations[i].y;
            float x_left = locations[i].x - 1;
            float x_right = locations[i].x + 1;
            float y_up = locations[i].y + 1;
            float y_down = locations[i].y - 1;
            for (int j = 0; j < children; j++)
            {
                float jx = locations[j].x;
                float jy = locations[j].y;
                if (x_left == jx && y == jy)
                {
                    nodeList[i].left = nodeList[j];
                }
                else if (x_right == jx && y == jy)
                {
                    nodeList[i].right = nodeList[j];
                }
                else if (y_up == jy && x == jx)
                {
                    nodeList[i].up = nodeList[j];
                }
                else if (y_down == jy && x == jx)
                {
                    nodeList[i].down = nodeList[j];
                }
            }
        }

    }

}
