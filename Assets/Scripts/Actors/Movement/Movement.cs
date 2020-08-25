using System.Collections;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public Node myNode;
    public bool moving = false;
    public GridNodes grid;

    public bool MoveTo(Node n)
    {
        if (n == null || n.traverseCost >= 100 || n.occupied)
        {
            return false;
        }
        if (myNode != null)
        {
            myNode.occupied = false;
        }
        else
        {
            grid.NodeFromPosition(transform.position).occupied = false;
        }
        n.occupied = true;
        StartCoroutine(SmoothMove(this.transform.position, n.gameObject.transform.position));
        myNode = n;
        return true;
    }

    public void Spawn(Node n)
    {
        n.occupied = true;
        this.transform.position = n.gameObject.transform.position;
        myNode = n;
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

    public bool CanMove(out RaycastHit2D hit, Vector2 dir)
    {
        hit = Physics2D.Raycast((Vector2)transform.position, dir, 1f, LayerMask.GetMask("Default"), 0);
        if (hit.collider != null && ((hit.collider.tag == "NPC" && transform.tag != "NPC") || hit.collider.tag == "Interactable" || hit.collider.tag == "Player"))
        {
            Debug.Log("Attempting to interact with " + hit.collider.gameObject.name);
            return false;
        }
        return true;
    }
}
