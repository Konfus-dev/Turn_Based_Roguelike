using UnityEngine;

public class PlayerMovement : Movement, IMovement
{
    private bool moveable = true;
    private bool axisInUse = false;     //used to have GetButtonDown functionality while using an axis

    private void Update()
    {
        if (!GameManager.Instance.PlayersTurn && !moveable) return;

        if (myNode != null)
        {
            float x_movement = Input.GetAxisRaw("Horizontal");
            float y_movement = Input.GetAxisRaw("Vertical");
            if (x_movement != 0 || y_movement != 0)
            {
                if (!axisInUse && !moving)
                {
                    axisInUse = true;
                    Player.Instance.currentState = Player.PlayerState.Moving;
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
                    Player.Instance.currentState = Player.PlayerState.NotMoving;
                    GameManager.Instance.PlayersTurn = false;
                }
            }
            else
            {
                axisInUse = false;
                if (Input.GetButtonDown("Skip"))
                {
                    Debug.Log("Player Skipped turn");
                    GameManager.Instance.PlayersTurn = false;
                }
            }
        }
    }

    public void TryMove()
    {
        RaycastHit2D hit;

        bool canMove = false; // base.CanMove(out hit); <- should be apart of movement class to check if can move using the raycast toward player movement

        Interactable Interactable = null;
        Enemy Enemy = null;
        if (!canMove) Interactable = null;// hit.transform.GetComponent<Interactable>();
        if (!canMove) Enemy = null;// hit.transform.GetComponent<Enemy>();

        if (Interactable != null) OnCantMove(Interactable);
        else if (Enemy != null) OnCantMove(Enemy);

        Player.Instance.Check();

        GameManager.Instance.PlayersTurn = false; // this is how gamemanager should be called because it is a singleton you do not need to keep reference there is only one, 
                                                  // look into singleton design pattern, anything there is only one of should follow this pattern (player maybe should do this).
    }

    public void OnCantMove<T>(T component) where T : Component
    {
        Interactable interactable = component.GetComponent<Interactable>();

        if (interactable != null)
        {
            // interact with interactable
            interactable.Interact<Player>(Player.Instance);
        }
        else
        {
            // do stuff with enemy (if running into enemy player is prolly trying to attack enemy so maybe do something like:
            // enemy.HP -+ player.Damage;
            Enemy enemy = component.GetComponent<Enemy>();
        }
    }
}
