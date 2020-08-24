using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    public int maxMana = 100;
    public int damage = 0;
    public int armor = 0;

    public int currentHealth = 60;
    public int currentMana = 100;

    public float MoveTime = 0.1f;
    public float AggroDistance = 5;
    public int travelTurns = 1;

    public ActorMovement movement;

    private Animator Animator;

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

        //animator = GetComponent<Animator>();
        int startState = Random.Range(0, 2);

        //randomly decide if enemy starts in sleeping or wandering state
        //if (startState == 0) CurrentState = EnemyState.Sleeping;
        //else CurrentState = EnemyState.Wandering;

        GameManager.Instance.AddEnemyToList(this);

    }

    public void CheckIfDead()
    {
        
        if (currentHealth <= 0)
        {
            //TODO: play particle effect here

            GameManager.Instance.RemoveEnemyFromList(this);
            movement.myNode.occupied = false;
            Debug.Log(this.gameObject.name + " is very dead. Their family is mortified by the news. You monster..");
            gameObject.SetActive(false);
        }
        
    }

    public void Attack(Player player)
    {
        SetState(EnemyState.Attacking);
        player.Defense(damage);
    }

    public void Defense(int dmg)
    {
        SetState(EnemyState.TakingDamage);
        currentHealth -= dmg;
        CheckIfDead();
    }


}
