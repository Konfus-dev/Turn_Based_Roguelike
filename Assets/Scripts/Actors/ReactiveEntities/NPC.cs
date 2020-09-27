using UnityEngine;

public class NPC : ReactiveEntity
{
    public ActorStats enemyStats;
    public float moveTime = 0.1f;
    public float aggroDistance = 5;
    public int travelTurns = 1;
    public int inventorySize = 1;

    public ActorMovement movement;
    private Inventory inventory;

    private Animator animator;


    public enum ActionState
    {
        Idle,
        Sleeping,
        Fleeing,
        MovingToTarget,
        Wandering, 
        Attacking,
        TakingDamage,
        Healing,
        Guarding
    }

    public enum AllianceState
    {
        Ally,
        Friendly,
        Docile,
        Enemy
    }


    public ActionState actionState;
    public AllianceState allianceState;

    public bool SetState(ActionState state)
    {
        if (System.Enum.IsDefined(typeof(ActionState), state))
        {
            actionState = state;
            return true;
        }
        return false;
    }

    public ActionState GetState()
    {
        return actionState;
    }

    protected void Start()
    {
        movement = transform.GetComponent<ActorMovement>();
        this.inventory = new Inventory();

        this.inventory.size = inventorySize;

        //animator = GetComponent<Animator>();  
        int startState = Random.Range(0, 2);

        //randomly decide if enemy starts in sleeping or wandering state
        //if (startState == 0) CurrentState = EnemyState.Sleeping;
        //else CurrentState = EnemyState.Wandering;

        GameManager.Instance.AddNPCToList(this);

    }

    public void OnHealthChange(int loss, int gain)
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

                GameManager.Instance.RemoveNPCFromList(this);
                movement.myNode.occupied = false;
                Debug.Log(this.gameObject.name + " is very dead. Their family is mortified by the news. You monster..");
                gameObject.SetActive(false);
            }
        }
        else
        {
            enemyStats.currentHealth += gain;
        }
    }

    public void Attack(ReactiveEntity actor)
    {
        //Debug.Log("trying to attack player");
        //set up to where they can also attack npc's
        SetState(ActionState.Attacking);
        if (actor.GetComponent<Player>())
        {
            Player.Instance.Defense(this.enemyStats.damage, gameObject.name);
        }
        else
        {
            NPC enemy = actor.GetComponent<NPC>();
            if (enemy != null)
            {
                enemy.Defense(this.enemyStats.damage, gameObject.name);
            }
        }
    }

    public void Defense(int dmg, string enemyName)
    {
        SetState(ActionState.TakingDamage);
        if (dmg - enemyStats.armor >= 0)
        {
            OnHealthChange(dmg - enemyStats.armor, 0);
        }
    }

    public override void Interact<T>(T component)
    {
        ReactiveEntity interactable = component.GetComponent<ReactiveEntity>();
        if (interactable != null)
        {
            Player pl = interactable.GetComponent<Player>();
            if (pl != null)
            {
                //Debug.Log(pl.gameObject.name + " attacking " + gameObject.name);
                pl.Attack(this);
            }
        }
    }
}
