using System.Collections;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public Node myNode;
    public bool moving = false;
    public GridNodes grid;

    public bool MoveTo(Node n)
    {
        if (n == null || n.traverseCost >= 100)
        {
            return false;
        }
        if (!(transform.tag == "Player" && Player.Instance.GetState() == Player.PlayerState.Ghosting))
        {
            if (n.occupied)
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
        }
        StartCoroutine(SmoothMove(this.transform.position, n.gameObject.transform.position));
        myNode = n;
        return true;
    }
    public bool MoveTo(Node n, float time)
    {
        if (n == null || n.traverseCost >= 100)
        {
            return false;
        }
        if (!(transform.CompareTag("Player") && Player.Instance.GetState() == Player.PlayerState.Ghosting))
        {
            if (n.occupied)
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
        }
        StartCoroutine(SmoothMove(this.transform.position, n.gameObject.transform.position, time));
        myNode = n;
        return true;
    }

    /*
    public void Spawn(Node n)
    {
        n.occupied = true;
        this.transform.position = n.gameObject.transform.position;
        myNode = n;
    }*/

    public IEnumerator SmoothMove(Vector3 origin, Vector3 destination)
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
    public IEnumerator SmoothMove(Vector3 origin, Vector3 destination, float time)
    {
        float currentMovementTime = 0f; //The amount of time that has passed
        if (time > 0)
        {
            while (Vector3.Distance(transform.localPosition, destination) > 0)
            {
                currentMovementTime += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(origin, destination, currentMovementTime / time);
                moving = true;
                yield return null;
            }
        }
        else
        {
            transform.localPosition = destination;
        }
        moving = false;
    }

    public bool CanMove(out RaycastHit2D hit, Vector2 dir)
    {
        hit = Physics2D.Raycast((Vector2)transform.position, dir, 1f, LayerMask.GetMask("Default"), 0);
        if (hit.collider != null)
        {
            if (transform.CompareTag("Player") && hit.transform.GetComponent<Statue>())
            {
                Debug.Log("Statue: you are a ghost? " + (Player.Instance.GetState() == Player.PlayerState.Ghosting));
                return !(Player.Instance.GetState() == Player.PlayerState.Ghosting);
            }
            if (((hit.collider.CompareTag("NPC") && !transform.CompareTag("NPC")) || hit.collider.CompareTag("Interactable") || hit.collider.CompareTag("Player")))
            {
                if (hit.collider.CompareTag("Player") && Player.Instance.GetState() == Player.PlayerState.Ghosting)
                {
                    return true;
                }
                Debug.Log("Attempting to interact with " + hit.collider.gameObject.name);
                return false;
            }
        }
        return true;
    }
}
