using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set;  }


    public Sprite[] Weapons;
    public Sprite[] Armor;
    public Sprite[] Potions;
    public Sprite[] Valuables;
    public Sprite[] Other;

    public Dictionary<string, Sprite> SpriteDictionary;

    public Transform ItemWorld;

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
        for (int i = 0; i < Potions.Length; i++)
        {
            Instance.SpriteDictionary.Add(Potions[i].name, Potions[i]);
        }
        for (int i = 0; i < Valuables.Length; i++)
        {
            Instance.SpriteDictionary.Add(Valuables[i].name, Valuables[i]);
        }
        for (int i = 0; i < Other.Length; i++)
        {
            Instance.SpriteDictionary.Add(Other[i].name, Other[i]);
        }
    }

}
