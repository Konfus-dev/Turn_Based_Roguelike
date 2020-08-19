using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Node myNode;
    bool moveable = true;
    bool axisInUse = false;     //used to have GetButtonDown functionality while using an axis

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (myNode != null)
        {
            if (moveable)
            {
                float x_movement = Input.GetAxisRaw("Horizontal");
                float y_movement = Input.GetAxisRaw("Vertical");
                if (x_movement != 0 || y_movement != 0)
                {
                    if (!axisInUse)
                    {
                        axisInUse = true;
                        if (x_movement > 0)
                        {
                            MoveTo(myNode.right);
                        }
                        else if (x_movement < 0)
                        {
                            MoveTo(myNode.left);
                        }
                        else if (y_movement > 0)
                        {
                            MoveTo(myNode.up);
                        }
                        else if (y_movement < 0)
                        {
                            MoveTo(myNode.down);
                        }
                    }
                }
                else
                {
                    axisInUse = false;
                }
            }
        }
        else
        {

        }
    }

    public bool MoveTo(Node n)
    {
        if (n == null || n.traverseCost >= 100 || n.occupied) {
            return false;
        }
        if (myNode != null)
        {
            myNode.occupied = false;
        }
        n.occupied = true;
        this.transform.position = n.gameObject.transform.position;
        myNode = n;
        return true;
    }

    public void Spawn(Node n)
    {

        MoveTo(n);
        Debug.Log("o no");
    }
}
