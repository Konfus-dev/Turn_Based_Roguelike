

using UnityEngine;

public class Chest : Interactable
{

    //private Animator animator;                    //Used to store a reference to the interactables's animator component.

    void Awake()
    {

    }

    public override void Interact<T>(T component)
    {
        //Player player = component as Player;
        OpenChest();
    }

    public void OpenChest()
    {
        Debug.Log("Opened Chest!");
    }
}
