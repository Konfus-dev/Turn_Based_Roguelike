using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public string actorName;
    [TextArea(3, 20)]
    public string[] lines;
    public int textDisplaySpeed;
}
