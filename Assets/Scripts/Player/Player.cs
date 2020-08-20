using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;        //Allows us to use SceneManager

//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
   
    private int Damage = 1;                    
    private int HP = 2;
    private int InventorySize = 5;

    [SerializeField]
    private InventoryUI InventoryUI;

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

    //public Inventory Inventory;             

    private void Start()
    {
        Instance = this;
        CurrentState = PlayerState.NotMoving;

        this.Inventory = new Inventory(UseItem);
        this.Inventory.Size = InventorySize;
        this.InventoryUI.SetInventory(this.Inventory);
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

       /* int horizontal; 
        int vertical;  

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            if (vertical > 0)
            {
                //move up
            }
            else if (vertical < 0)
            {
                //move down
            }
            else if (horizontal > 0)
            {
                //move right
            }
            else if (horizontal < 0)
            {
                //move left
            }
        }*/
    }

    private void UseItem(Item item)
    {
        Debug.Log(item.Amount);
        //put what items do here in switch statements I guess
        if(item.Type == Item.ItemType.Consumable) 
            Inventory.RemoveItem(new Item { Type = item.Type, Name = item.Name, Amount = 1} );
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
        //Debug.Log(this.Inventory.GetItems().Count);

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

    public void ManageHealth(int loss, int gain)
    {
        if (loss > 0)
        {
            //animator.SetTrigger("Hit");

            HP -= loss;

            CheckIfGameOver();
        }
        else
        {
            //Set the trigger for player get health
            //somthing to do with a health particle effect probably

            HP += gain;
        }
    }

    public void ManageDamage(int amountToAddToTotalDamage, int amountToSubFromTotalDamage)
    {
        if (amountToAddToTotalDamage > 0)
        {
            Damage += amountToAddToTotalDamage;
        }
        else
        {
            Damage -= amountToSubFromTotalDamage;
        }
    }

    private void CheckIfGameOver()
    {
        if (HP <= 0)
        {
            //TODO: 
            //play particle effect here
            //Call the GameOver function of GameManager.
            //GameManager.instance.GameOver();
        }
    }
}