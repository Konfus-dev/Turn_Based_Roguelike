using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject heart;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public static HealthUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        SyncHealth();
    }

    public void AddHearts(int num)
    {
        for(int h = 0; h < num; h++)
        {
            GameObject _heart = Instantiate(heart);
            _heart.transform.parent = this.transform;
            _heart.transform.localScale = heart.transform.localScale;
        }
    }

    public void SyncHealth()
    {
        if (this.transform.childCount < Player.Instance.playerStats.maxHealth)
        {
            AddHearts((Player.Instance.playerStats.maxHealth / 2) - this.transform.childCount);
        }

        int currPlayerHealth = Player.Instance.playerStats.currentHealth;
        int maxPlayerHealth = Player.Instance.playerStats.maxHealth;

        if (currPlayerHealth == maxPlayerHealth)
        {
            this.transform.GetChild(this.transform.childCount - 1).GetComponent<Image>().sprite = fullHeart;
            return;
        }

        if (currPlayerHealth % 2 == 0)
        {
            for (int h = (currPlayerHealth/2); h < this.transform.childCount; h++)
            {
                this.transform.GetChild(h).GetComponent<Image>().sprite = emptyHeart;
            }
        }
        else
        {
            this.transform.GetChild((currPlayerHealth / 2)).GetComponent<Image>().sprite = halfHeart;
            for (int h = currPlayerHealth / 2 + 1; h < this.transform.childCount; h++)
            {
                this.transform.GetChild(h).GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }
        
}
