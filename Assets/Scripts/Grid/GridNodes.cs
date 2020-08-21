using System.Collections.Generic;
using UnityEngine;

public class GridNodes : MonoBehaviour
{
    bool checkItems = true;
    public Node[] nodeList;
    public Vector3[] locations;
    public Dictionary<Vector3, Node> nodeFromLocation = new Dictionary<Vector3, Node>();

    void Update()
    {
        if (checkItems)
        {
            checkItems = false;
            MakeChildrenUseful();
            PlayerMovement playerMovement = Player.Instance.GetComponent<PlayerMovement>();
            playerMovement.Spawn(NodeFromPosition(playerMovement.transform.position));
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
                nodeFromLocation.Add(child.localPosition, n);
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

    public Node NodeFromPosition(Vector3 pos)
    {
        Vector3 key = new Vector3(Mathf.Floor(pos.x) + 0.5f, Mathf.Floor(pos.y) + 0.5f, 0);
        if (nodeFromLocation.TryGetValue(key, out Node n))
        {
            return n;
        }
        return null;
    }

    public List<Node> GetNeighboringNodes(Node n)
    {
        if (n == null)
        {
            return null;
        }
        List<Node> NeighboringNodes = new List<Node>();
        if (n.left != null)
        {
            NeighboringNodes.Add(n.left);
        }
        if (n.right != null)
        {
            NeighboringNodes.Add(n.right);
        }
        if (n.up != null)
        {
            NeighboringNodes.Add(n.up);
        }
        if (n.down != null)
        {
            NeighboringNodes.Add(n.down);
        }
        return NeighboringNodes;
    }

}
