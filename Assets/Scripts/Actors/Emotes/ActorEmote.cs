using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorEmote : MonoBehaviour
{
    private SpriteRenderer emoteRend;

    private void Awake()
    {
        emoteRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void Emote(string emote)
    {
        emoteRend.sprite = EmoteDatabase.Instance.emoteDictionary[emote];
        emoteRend.enabled = true;
        StartCoroutine(emoteShowTime(.5f));
    }

    public IEnumerator emoteShowTime(float emoteTime)
    {
        yield return new WaitForSeconds(emoteTime);
        emoteRend.enabled = false;
    }
}
