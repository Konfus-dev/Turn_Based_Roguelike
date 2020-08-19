using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node left, up, right, down;
    //0 for no cost, 10 for a door, 50 for mud or something, 100 for wall
    public int traverseCost = 0;
    public string name;
    public bool occupied = false;
    
    public Node()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(this.gameObject.name);
        if (name.Length > 0)
        {
            this.gameObject.name = name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
