
public class Chest : Interactable
{

    //private Animator animator;                    //Used to store a reference to the interactables's animator component.
    private Item ChestItem;

    void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    public override void Interact<T>(T component)
    {
        //Player player = component as Player;

        OpenChest();
    }

    private void OpenChest()
    {
        Destroy(gameObject);
        ItemWorld.DropItem(this.transform.position, ChestItem);
    }
}
