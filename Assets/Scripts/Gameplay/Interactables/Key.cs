
public class Key : Interactable
{
    // Start is called before the first frame update
    public override void Interact<T>(T component)
    {
        gameObject.SetActive(false);
    }
}
