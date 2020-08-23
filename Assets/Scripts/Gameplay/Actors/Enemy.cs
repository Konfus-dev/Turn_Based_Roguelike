using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int HP = 1;             
    public int Damage = 1;
    public float MoveTime = 0.1f;
    public float AggroDistance = 5;
    public int travelTurns = 1;
    public int InventorySize = 1;

    public ActorMovement movement;
    private Inventory Inventory;

    private Animator Animator;
    Vector3 MoveTo;

    public enum EnemyState
    {
        Sleeping,
        Moving,
        Wandering,
        Attacking,
        TakingDamage,
        Healing
    }


    public EnemyState CurrentState;

    public bool SetState(EnemyState state)
    {
        if (System.Enum.IsDefined(typeof(EnemyState), state))
        {
            CurrentState = state;
            return true;
        }
        return false;
    }

    public EnemyState GetState()
    {
        return CurrentState;
    }

    protected void Start()
    {
        movement = transform.GetComponent<ActorMovement>();
        this.Inventory = new Inventory(null);

        this.Inventory.Size = InventorySize;

        //animator = GetComponent<Animator>();
        int startState = Random.Range(0, 2);

        //randomly decide if enemy starts in sleeping or wandering state
        //if (startState == 0) CurrentState = EnemyState.Sleeping;
        //else CurrentState = EnemyState.Wandering;

        GameManager.Instance.AddEnemyToList(this);

    }

    public void ManageHealth(int loss, int gain)
    {
        //Set the trigger for the player animator to transition to the playerHit animation.
        //animator.SetTrigger("Hit");
        if (loss > 0)
        {
            //animator.SetTrigger("Hit");
            HP -= loss;

            if (HP <= 0)
            {
                //TODO: play particle effect here

                GameManager.Instance.RemoveEnemyFromList(this);
                movement.myNode.occupied = false;
                Debug.Log(this.gameObject.name + " is very dead. Their family is mortified by the news. You monster..");
                gameObject.SetActive(false);
            }
        }
        else
        {
            //Set the trigger for player get health
            //somthing to do with a health particle effect probably
            HP += gain;
        }
    }

    public void Attack(Player player)
    {
        SetState(EnemyState.Attacking);
        player.Defense(Damage);
    }

    public void Defense(int dmg)
    {
        SetState(EnemyState.TakingDamage);
        ManageHealth(dmg, 0);
    }


}
