using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Player : ReactiveEntity
{
    public ActorStats playerStats;
    public PlayerInventory inventories;
    DataManager playerData;

    [SerializeField]
    private Sprite[] sprites;

    private bool checkedAtStartOfTurn = false;

    public static Player Instance = null;

    public enum PlayerState
    {
        Idle,
        Moving,
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
            //if ((int)state < sprites.Length)
            //{
            //    transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[(int)state];
            //    
            //}
            if (GetState() == PlayerState.Attacking)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            else if (GetState() == PlayerState.Ghosting)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[2];
            }
            else
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            return true;
        }
        return false;
    }

    public bool SetState(PlayerState state, bool overwrite)
    {
        if ((overwrite || GetState() != PlayerState.Ghosting) && System.Enum.IsDefined(typeof(PlayerState), state))
        {
            CurrentState = state;
            //if ((int)state < sprites.Length)
            //{
            //    transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[(int)state];
            //    
            //}
            if (GetState() == PlayerState.Attacking)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            else if (GetState() == PlayerState.Ghosting)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[2];
            }
            else
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            return true;
        }
        return false;
    }

    public PlayerState GetState()
    {
        return CurrentState;
    }

    private void Awake()
    {

        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

    }

    private void Start()
    {
        inventories = this.GetComponent<PlayerInventory>();

        CurrentState = PlayerState.Idle;

        if (GameManager.Instance.initialScene)
        {
            InitializeData();
        }
        else
        {
            ReadData();
        }
    }

    private void ReadData()
    {
        playerData = Resources.Load<DataManager>("Data/PlayerData");
        gameObject.name = playerData.entityName;
        inventories.inventory.itemsData = playerData.entityInventory;
        inventories.equippedItems.itemsData = playerData.entityEquippedItems;
        playerStats = playerData.entityStats;

        PlayerMovement pm = transform.GetComponent<PlayerMovement>();
        if (pm.grid.SetupPlayer(playerData.loadingPosition))
        {
            Debug.Log("Found node at valid position");
        }
        else if (pm.grid.SetupPlayer())
        {
            Debug.Log("No node found at valid position. Found node at generic scene starting position");
        }
        else
        {
            Debug.Log("No nodes found. Perhaps the archives are incomplete");
        }
        Check();
    }

    public void WriteData()
    {
        playerData = Resources.Load<DataManager>("Data/PlayerData");
        playerData.entityName = gameObject.name;
        playerData.entityInventory = inventories.inventory.itemsData;
        playerData.entityEquippedItems = inventories.equippedItems.itemsData;
        playerData.entityStats = playerStats;
        playerData.loadingPosition = transform.position;
    }

    private void WriteData(Vector3 pos)
    {
        playerData = Resources.Load<DataManager>("Data/PlayerData");
        playerData.entityName = gameObject.name;
        playerData.entityInventory = inventories.inventory.itemsData;
        playerData.entityEquippedItems = inventories.equippedItems.itemsData;
        playerData.entityStats = playerStats;
        playerData.loadingPosition = pos;
    }

    public void InitializeData()
    {
        playerData = Resources.Load<DataManager>("Data/PlayerData");
        playerData.entityName = gameObject.name;
        playerData.entityInventory = inventories.inventory.itemsData;
        playerData.entityEquippedItems = inventories.equippedItems.itemsData;
        playerData.entityStats = playerStats;
        playerData.loadingPosition = transform.position;
        ForceSerialization();
    }
    void ForceSerialization()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(playerData);
        #endif
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



    public void StartScene(int scene, Vector3 pos)
    {
        GameManager.Instance.ClearNPCs();
        WriteData(pos);
        GameManager.Instance.initialScene = false;
        SceneManager.LoadScene(scene);
    }

    public void OnHealthChange(int loss, int gain)
    {
        //Set the trigger for the player animator to transition to the playerHit animation.
        //animator.SetTrigger("Hit");
        if (loss > 0)
        {
            //animator.SetTrigger("Hit");
            playerStats.currentHealth -= loss;

            if (playerStats.currentHealth <= 0)
            {
                //TODO: play particle effect here
                Debug.Log("You were killed by " + playerData.latestAttacker);
                Debug.Log("Heaven has not gained a new angel. Go revive yourself");
                Check();
            }
        }
        else
        {
            playerStats.currentHealth += gain;
        }
    }

    public void Attack(NPC enemy)
    {
        //Debug.Log("Attempting to attack " + enemy.name);
        if (SetState(PlayerState.Attacking))
        {
            playerData.latestAttackedByMe = enemy.gameObject.name;
            Debug.Log("Attacking " + enemy.name);
            enemy.Defense(playerStats.damage, name);
            //enemy.enemyStats.currentHealth -= playerStats.damage - enemy.enemyStats.armor;
        }
    }

    public void Defense(int dmg, string enemyName)
    {
        SetState(PlayerState.TakingDamage);
        playerData.latestAttacker = enemyName;
        if (dmg - playerStats.armor >= 0)
        {
            OnHealthChange(dmg - playerStats.armor, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item itemWorld = collision.GetComponent<Item>();
        if (GetState() != PlayerState.Ghosting && itemWorld != null && (this.inventories.inventory.GetItems().Count < this.inventories.inventory.size) ) 
        {
            bool selfDestruct = this.inventories.inventory.AddItem(itemWorld.GetItem(), true);
            if (selfDestruct) itemWorld.SelfDestruct();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReactiveEntity interactable = collision.gameObject.GetComponent<ReactiveEntity>();
        if (interactable != null)
        {
            collision.gameObject.GetComponent<ReactiveEntity>().Interact<Player>(this);
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
            GetComponent<PlayerMovement>().myNode.occupied = false;
        }
        else
        {
            SetState(PlayerState.Idle, true);
        }
    }

    public override void Interact<T>(T component)
    {
        //throw new System.NotImplementedException();
        // do stuff with enemy (if running into enemy player is prolly trying to attack enemy so maybe do something like:
        // enemy.HP -= player.Damage;
        if (Player.Instance.GetState() != Player.PlayerState.Ghosting)
        {
            NPC enemy = component.GetComponent<NPC>();
            if (enemy != null)
            {
                //Debug.Log(enemy.gameObject.name + " attacking " + gameObject.name);
                enemy.Attack(this);
            }
        }
    }
}