using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNewScene : ReactiveEntity
{
    public Vector2Int moveBetweenScenes;
    public Vector2 location1, location2;

    //private Animator animator;                    //Used to store a reference to the interactables's animator component.
    
    void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    public override void Interact<T>(T component)
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        if (scene == moveBetweenScenes.x)
        {
            OpenScene(moveBetweenScenes.y, new Vector3(location2.x, location2.y, 0));
        }
        else if (scene == moveBetweenScenes.y)
        {
            OpenScene(moveBetweenScenes.x, new Vector3(location1.x, location1.y, 0));
        }
        else
        {
            Debug.Log("Door is broken");
        }
    }

    private void OpenScene(int s, Vector3 pos)
    {
        //make a door open sound
        Debug.Log("Entering scene " + s);
        Player.Instance.StartScene(s, pos);
    }
}
