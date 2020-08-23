using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour/*, IComparable<Node>*/
{
    public Node left, up, right, down;
    //0 for no cost, 10 for a door, 50 for mud or something, 100 for wall
    public int traverseCost = 0;
    public string name;
    public bool occupied = false;

    public float gCost = 0;
    public float hCost = 0;
    public float FCost { get { return gCost + hCost; } }
    public Node Parent;
    
    public Node()
    {

    }

    public override string ToString()
    {
        return "Node " + gameObject.name + ": " + transform.position;
    }

    void Start()
    {
        if (name.Length > 0)
        {
            this.gameObject.name = name;
        }
    }

}
