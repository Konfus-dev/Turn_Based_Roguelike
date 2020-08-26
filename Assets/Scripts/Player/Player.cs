using UnityEngine;

public class Player : Interactable
{
    public ActorStats playerStats;
    public PlayerInventory inventories;

    [SerializeField]
    private Sprite[] sprites;

    private bool checkedAtStartOfTurn = false;

    public static Player Instance = null;

    public enum PlayerState
    {
        Moving,
        NotMoving,
        Interacting,
        Attacking,
        TakingDamage,
        Ghosting,
        InMenu
    }

    public PlayerState CurrentState;

    public bool SetState(PlayerState state)
    {
        if (GetState() != PlayerState.Ghosting && System.Enum.IsDefined(typeof(PlayerState), state))
        {
            CurrentState = state;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[(int)state];
            return true;
        }
        return false;
    }

    public PlayerState GetState()
    {
        return CurrentState;
    }

    private void Start()
    {
        inventories = this.GetComponent<PlayerInventory>();

        CurrentState = PlayerState.NotMoving;

        Instance = this;

        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
        //When Player object is disabled, store stuff in the GameManager so it can be re-loaded in next level.
    }

    private void Update()
    {
        if (!GameManager.Instance.PlayersTurn)
        {
            checkedAtStartOfTurn = false;
            return;
        }
        else if (!checkedAtStartOfTurn)
        {
            checkedAtStartOfTurn = true;
            Check();
        }
        CheckStats();
    }

    public void Check()
    {
        //any checks that need done put in here (check should be performed at the start of a turn and end) 
        CheckIfDead();
    }

    public void Attack(NPC enemy)
    {
        enemy.enemyStats.currentHealth -= playerStats.damage - enemy.enemyStats.armor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item itemWorld = collision.GetComponent<Item>();
        if (GetState() != PlayerState.Ghosting && itemWorld != null && (this.inventories.inventory.GetItems().Count < this.inventories.inventory.size || itemWorld.itemData.IsStackable()) ) 
        {
            this.inventories.inventory.AddItem(itemWorld.GetItem());
            itemWorld.SelfDestruct();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Interactable interactable = collision.gameObject.GetComponent<Interactable>();
        if (interactable != null)
        {
            collision.gameObject.GetComponent<Interactable>()
                .Interact<Player>(this);
        }
    }

    private void CheckStats()
    {
        if (playerStats.currentMana > playerStats.maxMana) playerStats.currentMana = playerStats.maxMana;
        if (playerStats.currentHealth > playerStats.maxHealth) playerStats.currentHealth = playerStats.maxHealth;
    }

    private void CheckIfDead()
    {
        if (playerStats.currentHealth <= 0)
        {
            //TODO: 
            //play particle effect here
            //Call the GameOver function of GameManager.
            //GameManager.instance.GameOver();

            SetState(PlayerState.Ghosting);

        }
    }

    public override void Interact<T>(T component)
    {
        throw new System.NotImplementedException();
    }
}