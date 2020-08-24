using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxHealth = 100;
    public float maxMana = 100;
    public float maxDamage = 0;
    public float maxArmor = 0;

    public float currentHealth = 60;
    public float currentMana = 100;
    public float currentDamage = 0;
    public float currentArmor = 0;

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
        Healing,
        InMenu
    }

    public PlayerState currentState;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Instance = this;
        currentState = PlayerState.NotMoving;

        //Get a component reference to the Player's animator component
        //animator = GetComponent<Animator>();
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
        CheckForPlayerPause();
    }

    private void CheckForPlayerPause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!(currentState == PlayerState.InMenu))
            {
                
                currentState = PlayerState.InMenu;
            }
            else
            {
                
                currentState = PlayerState.NotMoving;
            }
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

    public void Check()
    {
        //any checks that need done put in here (check should be performed at the start of a turn and end) 

        CheckIfGameOver();
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
    

    private void CheckIfGameOver()
    {
        if (currentHealth <= 0)
        {
            //TODO: 
            //play particle effect here
            //Call the GameOver function of GameManager.
            //GameManager.instance.GameOver();
        }
    }
}