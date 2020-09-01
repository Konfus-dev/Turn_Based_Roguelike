using UnityEngine;

public class PlayerMovement : Movement, IMovement
{
    private bool moveable = true;

    private void Update()
    {
        if (!GameManager.Instance.PlayersTurn || !moveable) return;

        if (myNode != null)
        {
            float x_movement = Input.GetAxisRaw("Horizontal");
            float y_movement = Input.GetAxisRaw("Vertical");
            if (x_movement != 0 || y_movement != 0)
            {
                if (!moving)
                {
                    Player.Instance.SetState(Player.PlayerState.Moving);
                    if (x_movement > 0 && TryMove(Vector2.right))
                    {
                        MoveTo(myNode.right);
                    }
                    else if (x_movement < 0 && TryMove(Vector2.left))
                    {
                        MoveTo(myNode.left);
                    }
                    else if (y_movement > 0 && TryMove(Vector2.up))
                    {
                        MoveTo(myNode.up);
                    }
                    else if (y_movement < 0 && TryMove(Vector2.down))
                    {
                        MoveTo(myNode.down);
                    }
                    Player.Instance.SetState(Player.PlayerState.Idle);
                    GameManager.Instance.PlayersTurn = false;
                }
            }
            else
            {
                if (Input.GetButton("Skip"))
                {
                    Debug.Log("Player Skipped turn");
                    GameManager.Instance.PlayersTurn = false;
                }
            }
        }
    }

    public bool TryMove(Vector2 dir)
    {
        RaycastHit2D hit;

        bool canMove = CanMove(out hit, dir); // base.CanMove(out hit); <- should be apart of movement class to check if can move using the raycast toward player movement

        ReactiveEntity Interactable = null;

        if (!canMove) Interactable = hit.transform.GetComponent<ReactiveEntity>();

        if (Interactable != null) OnCantMove(Interactable);

        Player.Instance.Check();

        GameManager.Instance.PlayersTurn = false; // this is how gamemanager should be called because it is a singleton you do not need to keep reference there is only one, 
                                                  // look into singleton design pattrn, anything there is only one of should follow this pattern (player maybe should do this).
        return canMove;
    }

    public void OnCantMove<T>(T component) where T : Component
    {
        if (Player.Instance.GetState() == Player.PlayerState.Ghosting) return;
        ReactiveEntity interactable = component.GetComponent<ReactiveEntity>();

        if (interactable != null)
        {
            // interact with interactable
            interactable.Interact<Player>(Player.Instance);
            Debug.Log(transform.gameObject.name + " interacting with: " + interactable.transform.gameObject.name);
        }
        /*else
        {
            // do stuff with enemy (if running into enemy player is prolly trying to attack enemy so maybe do something like:
            // enemy.HP -+ player.Damage;
            if (Player.Instance.GetState() != Player.PlayerState.Ghosting)
            {
                NPC enemy = component.GetComponent<NPC>();
                Debug.Log(transform.gameObject.name + " attacking npc: " + enemy.transform.gameObject.name);
                Player.Instance.Attack(enemy);
            }
        }*/
    }
}
