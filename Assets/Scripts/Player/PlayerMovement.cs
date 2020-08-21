using System.Collections;
using UnityEngine;

public class PlayerMovement : Movement
{
    bool moveable = true;
    bool axisInUse = false;     //used to have GetButtonDown functionality while using an axis
    public GameManager gM;

     
    void Update()
    {

        if (myNode != null)
        {
            if (gM.PlayersTurn && moveable)
            {
                float x_movement = Input.GetAxisRaw("Horizontal");
                float y_movement = Input.GetAxisRaw("Vertical");
                if (x_movement != 0 || y_movement != 0)
                {
                    if (!axisInUse && !moving)
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
                        gM.PlayersTurn = false;
                    }
                }
                else
                {
                    axisInUse = false;
                    if (Input.GetButtonDown("Skip"))
                    {
                        Debug.Log("Player Skipped turn");
                        gM.PlayersTurn = false;
                    }
                }
            }
        }
        else
        {

        }
    }
}
