using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Node myNode;
    bool moveable = true;
    bool axisInUse = false;     //used to have GetButtonDown functionality while using an axis
    bool moving = false;

    void Start()
    {
        
    }

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
        StartCoroutine(SmoothMove(this.transform.position, n.gameObject.transform.position));
        //this.transform.position = n.gameObject.transform.position;
        myNode = n;
        return true;
    }

    public void Spawn(Node n)
    {

        n.occupied = true;
        this.transform.position = n.gameObject.transform.position;
        myNode = n;
        Debug.Log("o no");
    }

    private IEnumerator SmoothMove(Vector3 origin, Vector3 destination)
    {
        float totalMovementTime = .1f; //the amount of time you want the movement to take
        float currentMovementTime = 0f; //The amount of time that has passed
        while (Vector3.Distance(transform.localPosition, destination) > 0)
        {
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(origin, destination, currentMovementTime / totalMovementTime);
            moving = true;
            yield return null;
        }
        moving = false;
    }
}
