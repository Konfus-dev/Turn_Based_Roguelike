using UnityEngine;

public class ActorMovement : Movement, IMovement
{
    private NodePathfinding pathfinding;
    private Transform Target;
    public NPC actor;
    public Vector2 direction;

    void Start()
    {
        pathfinding = transform.GetComponent<NodePathfinding>();
        Target = Player.Instance.transform;
        actor = transform.GetComponent<NPC>();
    }

    public bool MoveEnemy()
    {
        if (actor.GetState() == NPC.MovementState.Sleeping)
        {
            return false;
        }
        float distance = Vector3.Distance(Target.transform.position, this.transform.position);

        if (distance <= actor.aggroDistance && Player.Instance.GetState() != Player.PlayerState.Ghosting
            && actor.SetState(NPC.MovementState.MovingToTarget))
        {
            Node bestFootForward = pathfinding.FindStep(transform.position, Target.position);
            if (bestFootForward != null)
            {
                if (bestFootForward == myNode.left)
                {
                    direction = Vector2.left;
                }
                else if (bestFootForward == myNode.right)
                {
                    direction = Vector2.right;
                }
                else if (bestFootForward == myNode.up)
                {
                    direction = Vector2.up;
                }
                else if (bestFootForward == myNode.down)
                {
                    direction = Vector2.down;
                }
                else
                {
                    direction = Vector2.zero;
                }
                if (TryMove(direction))
                {
                    MoveTo(bestFootForward);
                    return true;
                }
            }
            return false;
        }
        else if (actor.SetState(NPC.MovementState.Wandering))
        {
            RandomMovePosWeighted();
            return true;
        }
        return false;
    }

    /*private void RandomMovePos()
    {
        Random rnd = new Random();
        int rand = Random.Range(0, 5);
        if (myNode != null)
        {
            if (rand == 0)
                MoveTo(myNode.left);
            else if (rand == 1)
                MoveTo(myNode.right);
            else if (rand == 2)
                MoveTo(myNode.up);
            else if (rand == 3)
                MoveTo(myNode.down);
        }
    }*/

    private void RandomMovePosWeighted()
    {
        float leftCost = 0;
        float rightCost = 0;
        float upCost = 0;
        float downCost = 0;
        if (myNode != null)
        {
            if (myNode.left != null)
            {
                leftCost = (100 - myNode.left.traverseCost);
            }
            if (myNode.right != null)
            {
                rightCost = (100 - myNode.right.traverseCost);
            }
            if (myNode.up != null)
            {
                upCost = (100 - myNode.up.traverseCost);
            }
            if (myNode.down != null)
            {
                downCost = (100 - myNode.down.traverseCost);
            }
            float costSum = leftCost + rightCost + upCost + downCost;
            float rand = Random.Range(0f, 1f);
            float asWeGo = 0;
            if (rand >= asWeGo && rand < asWeGo + leftCost / costSum && TryMove(Vector2.left))
            {
                MoveTo(myNode.left);
            }
            asWeGo += leftCost / costSum;
            if (rand >= asWeGo && rand < asWeGo + rightCost / costSum && TryMove(Vector2.right))
            {
                MoveTo(myNode.right);
            }
            asWeGo += rightCost / costSum;
            if (rand >= asWeGo && rand < asWeGo + upCost / costSum && TryMove(Vector2.up))
            {
                MoveTo(myNode.up);
            }
            asWeGo += upCost / costSum;
            if (rand >= asWeGo && rand < asWeGo + downCost / costSum && TryMove(Vector2.down))
            {
                MoveTo(myNode.down);
            }
            asWeGo += downCost / costSum;
        }
    }

    public bool TryMove(Vector2 dir)
    {
        RaycastHit2D hit;

        bool canMove = CanMove(out hit, dir); // base.CanMove(out hit); <- should be apart of movement class to check if can move using the raycast toward player movement

        ReactiveEntity Interactable = null;
        Player Player = null;
        if (!canMove) Interactable = hit.transform.GetComponent<ReactiveEntity>();
        if (!canMove) Player = hit.transform.GetComponent<Player>();

        if (Interactable != null) OnCantMove(Interactable);
        else if (Player != null && Player.Instance.GetState() != Player.PlayerState.Ghosting) OnCantMove(Player);

        return canMove;
    }

    public void OnCantMove<T>(T component) where T : Component
    {
        ReactiveEntity interactable = component.GetComponent<ReactiveEntity>();

        if (interactable != null)
        {
            // interact with interactable Have to decide if npcs can interact with stuff
            //interactable.Interact<Player>(Player.Instance);
            //Debug.Log(transform.gameObject.name + " interacting with: " + interactable.transform.gameObject.name);
        }
        else
        {
            // do stuff with enemy (if running into enemy player is prolly trying to attack enemy so maybe do something like:
            // enemy.HP -+ player.Damage;
            Player player = component.GetComponent<Player>();
            Debug.Log(transform.gameObject.name + " attacking player: " + player.transform.gameObject.name);
            actor.Attack(player);
        }
    }

}
