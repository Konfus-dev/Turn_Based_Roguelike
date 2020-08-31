using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteDatabase : MonoBehaviour
{ 
    public static EmoteDatabase Instance = null;

    public Sprite[] emotes;
    public string[] keys;

    public Dictionary<string, Sprite> emoteDictionary;

    void Awake()
    {

        emoteDictionary = new Dictionary<string, Sprite>();

        for(int e = 0; e < emotes.Length; e++)
        {
            emoteDictionary.Add(keys[e], emotes[e]);
        }

        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);
    }
}
