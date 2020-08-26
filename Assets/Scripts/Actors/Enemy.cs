using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ActorStats enemyStats;
    public float moveTime = 0.1f;
    public float aggroDistance = 5;
    public int travelTurns = 1;
    public int inventorySize = 1;

    public ActorMovement movement;
    private Inventory inventory;

    private Animator animator;

    public enum EnemyState
    {
        Ally,
        Friendly,
        Docile,
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
        this.inventory = new Inventory(null);

        this.inventory.size = inventorySize;

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
            enemyStats.currentHealth -= loss;

            if (enemyStats.currentHealth <= 0)
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
            enemyStats.currentHealth += gain;
        }
    }

    public void Attack(Player player)
    {
        SetState(EnemyState.Attacking);
        Player.Instance.playerStats.currentHealth -= this.enemyStats.damage - Player.Instance.playerStats.armor;
    }

    public void Defense(int dmg)
    {
        SetState(EnemyState.TakingDamage);
        ManageHealth(dmg, 0);
    }


}
