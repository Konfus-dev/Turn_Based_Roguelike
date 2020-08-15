using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{

    //private Animator animator;                    //Used to store a reference to the interactables's animator component.

    void Awake()
    {
        //animator = GetComponent<Animator>();
    }


    public override void Interact<T>(T component)
    {
        // Player player = component as Player;

        // OpenChest(player.Inventory);
    }

    private void OpenChest(/*Inventory playerInventory*/)
    {
        //Debug.Log("Opened Chest!");
    }
}
