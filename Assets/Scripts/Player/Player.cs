using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxMana = 100;
    public int damage = 0;
    public int armor = 0;

    public int currentHealth = 60;
    public int currentMana = 100;

    [SerializeField]
    private Sprite[] sprites;

    private bool CheckedAtStartOfTurn = false;


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

    public PlayerState CurrentState;

    public bool SetState(PlayerState state)
    {
        if (GetState() != PlayerState.Ghosting && System.Enum.IsDefined(typeof(PlayerState), state))
        {
            CurrentState = state;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[(int)state];
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

        CurrentState = PlayerState.NotMoving;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
        //When Player object is disabled, store stuff in the GameManager so it can be re-loaded in next level.
    }

    private void Update()
    {
        if (!GameManager.Instance.PlayersTurn)
        {
            CheckedAtStartOfTurn = false;
            return;
        }
        else if (!CheckedAtStartOfTurn)
        {
            CheckedAtStartOfTurn = true;
            Check();
        }
        CheckForPlayerPause();
    }

    private void CheckForPlayerPause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!(CurrentState == PlayerState.InMenu))
            {
                CurrentState = PlayerState.InMenu;
            }
            else
            {
                SetState(PlayerState.NotMoving);
                Check();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ItemInWorld itemWorld = collision.GetComponent<ItemInWorld>();
        if (GetState() != PlayerState.Ghosting) 
        {
            
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
        CheckIfDead();
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ManageDamage(int loss, int gain)
    {
        loss = Mathf.Abs(loss);
        gain = Mathf.Abs(gain);

        if (gain > 0)
        {
            damage += gain;
        }
        else
        {
            damage -= loss;
        }
    }

    public void Attack(Enemy enemy)
    {
        if (SetState(PlayerState.Attacking)) 
        {
            Debug.Log("Attacking " + GetState());
            enemy.Defense(damage);
        }
    }

    public void Defense(int dmg)
    {
        if (SetState(PlayerState.TakingDamage))
        {
            currentHealth -= dmg - armor;
        }
    }
   

    private void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            //TODO: 
            //play particle effect here
            //Call the GameOver function of GameManager.
            //GameManager.instance.GameOver();

            SetState(PlayerState.Ghosting);

        }
    }
}