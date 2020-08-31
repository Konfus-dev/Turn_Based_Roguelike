using UnityEngine;


[System.Serializable]
public class DialogueData
{
    public string name;
    [TextArea(3, 20)]
    public string[] lines;
    public int talkSpeed;
    public AudioClip voice;
}
