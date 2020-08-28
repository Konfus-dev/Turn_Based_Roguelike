using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncUICharacter : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponent<UnityEngine.UI.Image>().sprite = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
