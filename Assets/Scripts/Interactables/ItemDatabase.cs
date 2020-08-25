using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set;  }


    public Sprite[] Weapons;
    public Sprite[] Armor;
    public Sprite[] Tools;
    public Sprite[] Consumables;
    public Sprite[] Valuables;

    public Dictionary<string, Sprite> SpriteDictionary;

    public Transform ItemWorldTemplate;

    private void Awake()
    {
        Instance = this;

        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        Instance.SetUpInstance();
    }

    private void SetUpInstance()
    {
        SpriteDictionary = new Dictionary<string, Sprite>();

        for (int i = 0; i < Weapons.Length; i++)
        {
            SpriteDictionary.Add(Weapons[i].name, Weapons[i]);
        }
        for (int i = 0; i < Armor.Length; i++)
        {
            SpriteDictionary.Add(Armor[i].name, Armor[i]);
        }
        for (int i = 0; i < Tools.Length; i++)
        {
            SpriteDictionary.Add(Tools[i].name, Tools[i]);
        }
        for (int i = 0; i < Valuables.Length; i++)
        {
            SpriteDictionary.Add(Valuables[i].name, Valuables[i]);
        }
        for (int i = 0; i < Consumables.Length; i++)
        {
            SpriteDictionary.Add(Consumables[i].name, Consumables[i]);
        }
    }

}
