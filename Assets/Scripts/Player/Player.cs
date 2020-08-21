using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;        //Allows us to use SceneManager

//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField]
    private int Damage = 1;
    [SerializeField]
    private int CurrHP = 2;
    [SerializeField]
    private int MaxHP = 2;
    [SerializeField]
    private int Armor = 0;
    [SerializeField]
    private int InventorySize = 5;

    [SerializeField]
    private InventoryUI InventoryUI;

    private Inventory EquippedItems;
    private Inventory Inventory;

    public enum PlayerState
    {
        Moving,
        NotMoving,
        Interacting,
        Attacking,
        TakingDamage,
        Ghosting,
        Healing,
        InInventory
    }

    public PlayerState CurrentState;

    private void Start()
    {
        Instance = this;
        CurrentState = PlayerState.NotMoving;

        this.Inventory = new Inventory(UseItem);
        this.Inventory.Size = InventorySize;
        this.EquippedItems = new Inventory(null);
        this.EquippedItems.Size = 3;
        this.InventoryUI.SetInventory(this.Inventory);
        this.InventoryUI.SetEquippedItems(this.EquippedItems);
        this.InventoryUI.SetPlayer(this);
        //Get a component reference to the Player's animator component
        //animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        //When Player object is disabled, store stuff in the GameManager so it can be re-loaded in next level.
    }

    private void Update()
    {
        if (!GameManager.Instance.PlayersTurn || CurrentState == PlayerState.Moving) return;
        CheckForPlayerPause();
    }

    private void UseItem(Item item)
    {
        Debug.Log(item.Amount);

        //put what items do here in switch statements I guess
        if(item.Type == Item.ItemType.Consumable)
        {
            ManageCurrHealth(0, item.HealthMod);
            Inventory.RemoveItem(new Item { Type = item.Type, Name = item.Name, Amount = 1 });
        }
    }

    private void CheckForPlayerPause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!(CurrentState == PlayerState.InInventory))
            {
                InventoryUI.OpenInventory();
                CurrentState = PlayerState.InInventory;
            }
            else
            {
                InventoryUI.CloseInventory();
                CurrentState = PlayerState.NotMoving;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemInWorld itemWorld = collision.GetComponent<ItemInWorld>();
        if (itemWorld != null && this.Inventory.GetItems().Count < this.Inventory.Size)
        {
            this.Inventory.AddItem(itemWorld.GetItem());
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

    private void Check()
    {
        //any checks that need done put in here (check should be performed at the start of a turn and end) 

        CheckIfGameOver();

        GameManager.Instance.PlayersTurn = false;
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ManageCurrHealth(int loss, int gain)
    {
        if (loss > 0)
        {
            //animator.SetTrigger("Hit");

            CurrHP -= loss;

            CheckIfGameOver();
        }
        else
        {
            //Set the trigger for player get health
            //somthing to do with a health particle effect probably

            CurrHP += gain;
            if (CurrHP > MaxHP) CurrHP = MaxHP;
        }
    }

    public void ManageMaxHealth(int loss, int gain)
    {
        if (loss > 0)
        {
            //animator.SetTrigger("Hit");

            MaxHP -= loss;

            if (MaxHP <= 0) MaxHP = 1;
        }
        else
        {
            //Set the trigger for player get health
            //somthing to do with a health particle effect probably

            MaxHP += gain;
        }
    }

    public void ManageDamage(int loss, int gain)
    {
        if (gain > 0)
        {
            Damage += gain;
        }
        else
        {
            Damage -= loss;
        }
    }

    public void ManageArmor(int loss, int gain)
    {
        if (gain > 0)
        {
            Armor += gain;
        }
        else
        {
            Armor -= loss;
        }
    }

    private void CheckIfGameOver()
    {
        if (CurrHP <= 0)
        {
            //TODO: 
            //play particle effect here
            //Call the GameOver function of GameManager.
            //GameManager.instance.GameOver();
        }
    }
}