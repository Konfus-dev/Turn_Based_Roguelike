﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;        //Allows us to use SceneManager

//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int Damage = 1;                    
    public int HP;    

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
        Healing
    }

    public PlayerState CurrentState;

    //public Inventory Inventory;             

    private void Start()
    {
        Instance = this;
        CurrentState = PlayerState.NotMoving;

        this.Inventory = new Inventory();
        this.InventoryUI.SetInventory(this.Inventory);
        //Get a component reference to the Player's animator component
        //animator = GetComponent<Animator>();

        ItemWorld.SpawnItemWorld(new Vector3(20,20), new Item { Type = Item.ItemType.Weapon, Name = "Sword_6", Amount = 1 });
    }

    private void OnDisable()
    {
        //When Player object is disabled, store stuff in the GameManager so it can be re-loaded in next level.
    }

    private void Update()
    {
        if (!GameManager.Instance.PlayersTurn || CurrentState == PlayerState.Moving) return;

        int horizontal; 
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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Interactable"))
        {
            collision.gameObject.GetComponent<Interactable>()
                .Interact<Player>(this);
        }
    }

    private void Check()
    {
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