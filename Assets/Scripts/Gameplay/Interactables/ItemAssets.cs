using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set;  }


    public Sprite[] Weapons;
    public Sprite[] Armor;
    public Sprite[] Tools;
    public Sprite[] Consumables;
    public Sprite[] Valuables;

    public Dictionary<string, Sprite> SpriteDictionary;
    public string[] temp;

    public Transform ItemWorldTemplate;

    private void Awake()
    {
        Instance = this;
        SpriteDictionary = new Dictionary<string, Sprite>();
        SetUpInstance();
    }

    private void SetUpInstance()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            Instance.SpriteDictionary.Add(Weapons[i].name, Weapons[i]);
        }
        for (int i = 0; i < Armor.Length; i++)
        {
            Instance.SpriteDictionary.Add(Armor[i].name, Armor[i]);
        }
        for (int i = 0; i < Tools.Length; i++)
        {
            Instance.SpriteDictionary.Add(Tools[i].name, Tools[i]);
        }
        for (int i = 0; i < Valuables.Length; i++)
        {
            Instance.SpriteDictionary.Add(Valuables[i].name, Valuables[i]);
        }
        for (int i = 0; i < Consumables.Length; i++)
        {
            Instance.SpriteDictionary.Add(Consumables[i].name, Consumables[i]);
        }
        Instance.temp = SpriteDictionary.Keys.ToArray();
    }

}
