using UnityEngine;

public class Enemy : MonoBehaviour, IMovement
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

<<<<<<< HEAD:Assets/Scripts/Actors/Enemy.cs
    public ActorMovement movement;

    private Animator Animator;
=======
    private Inventory Inventory;

    private Animator Animator;                   
	private Transform Target;
    public ActorMovement movement;
    private NodePathfinding pathfinding;
    public GridNodes grid;
    Vector3 MoveTo;
>>>>>>> parent of 93f984c... Combat system and ghost-death working!:Assets/Scripts/Gameplay/Actors/Enemy.cs

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

    protected void Start()
    {
<<<<<<< HEAD:Assets/Scripts/Actors/Enemy.cs
        movement = transform.GetComponent<ActorMovement>();
=======
        this.Inventory = new Inventory(null);

        this.Inventory.Size = InventorySize;
>>>>>>> parent of 93f984c... Combat system and ghost-death working!:Assets/Scripts/Gameplay/Actors/Enemy.cs

        //animator = GetComponent<Animator>();
        int startState = Random.Range(0, 2);
        movement = transform.GetComponent<ActorMovement>();
        pathfinding = transform.GetComponent<NodePathfinding>();
        grid = pathfinding.grid;

        //randomly decide if enemy starts in sleeping or wandering state
        //if (startState == 0) CurrentState = EnemyState.Sleeping;
        //else CurrentState = EnemyState.Wandering;

        GameManager.Instance.AddEnemyToList(this);

        Target = Player.Instance.transform;
    }

    public void CheckIfDead()
    {
        
        if (currentHealth <= 0)
        {
<<<<<<< HEAD:Assets/Scripts/Actors/Enemy.cs
            //TODO: play particle effect here

            GameManager.Instance.RemoveEnemyFromList(this);
            movement.myNode.occupied = false;
            Debug.Log(this.gameObject.name + " is very dead. Their family is mortified by the news. You monster..");
            gameObject.SetActive(false);
=======
            //animator.SetTrigger("Hit");
            HP -= loss;

            if (HP <= 0)
            {
                //TODO: play particle effect here
                gameObject.SetActive(false);
            }
        }
        else
        {
            //Set the trigger for player get health
            //somthing to do with a health particle effect probably
            HP += gain;
>>>>>>> parent of 93f984c... Combat system and ghost-death working!:Assets/Scripts/Gameplay/Actors/Enemy.cs
        }
        
    }
	
	public bool MoveEnemy()
	{
        if (CurrentState == EnemyState.Sleeping)
        {
            return false;
        }
        float distance = Vector3.Distance(Target.transform.position, this.transform.position);
        
        if (distance <= AggroDistance)
        {
            CurrentState = EnemyState.Moving;
            Node bestFootForward = pathfinding.FindStep(transform.position, Target.position);
            if (bestFootForward != null)
            {
                movement.nextNode = bestFootForward;
                movement.MoveTo(bestFootForward);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            CurrentState = EnemyState.Wandering;
            RandomMovePos();
            return true;
        }
	}

    private void RandomMovePos()
    {
<<<<<<< HEAD:Assets/Scripts/Actors/Enemy.cs
        SetState(EnemyState.Attacking);
        player.Defense(damage);
=======
        Random rnd = new Random();
        int rand = Random.Range(0, 4);
        if (movement.myNode != null) {
            if (rand == 0)
                movement.MoveTo(movement.myNode.left);
            else if (rand == 1)
                movement.MoveTo(movement.myNode.right);
            else if (rand == 2)
                movement.MoveTo(movement.myNode.up);
            else if (rand == 3)
                movement.MoveTo(movement.myNode.down);
        }
>>>>>>> parent of 93f984c... Combat system and ghost-death working!:Assets/Scripts/Gameplay/Actors/Enemy.cs
    }

    public void TryMove()
    {
<<<<<<< HEAD:Assets/Scripts/Actors/Enemy.cs
        SetState(EnemyState.TakingDamage);
        currentHealth -= dmg;
        CheckIfDead();
=======
        RaycastHit2D hit;

        bool canMove = false; // base.CanMove(out hit); <- should be apart of movement class to check if can move using the raycast toward player movement

        Interactable Interactable = null;
        Enemy Enemy = null;
        if (!canMove) Interactable = null;// hit.transform.GetComponent<Interactable>();
        if (!canMove) Enemy = null;// hit.transform.GetComponent<Enemy>();

        if (Interactable != null) OnCantMove(Interactable); //maybe enemys can interact with things?
        else if (Enemy != null) OnCantMove(Enemy);

        Player.Instance.Check();

        GameManager.Instance.PlayersTurn = false; // this is how gamemanager should be called because it is a singleton you do not need to keep reference there is only one, 
                                                  // look into singleton design pattern, anything there is only one of should follow this pattern (player maybe should do this).
>>>>>>> parent of 93f984c... Combat system and ghost-death working!:Assets/Scripts/Gameplay/Actors/Enemy.cs
    }

    public void OnCantMove<T>(T component) where T : Component
    {
        Interactable interactable = component.GetComponent<Interactable>();

        if (interactable != null)
        {
            // interact with interactable
            interactable.Interact<Enemy>(this);
        }
        else
        {
            // do stuff with enemy (if running into enemy player is prolly trying to attack enemy so maybe do something like:
            // enemy.HP -+ player.Damage;
            Enemy enemy = component.GetComponent<Enemy>();
        }
    }
}
