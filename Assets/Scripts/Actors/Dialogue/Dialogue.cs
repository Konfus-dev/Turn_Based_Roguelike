using UnityEngine.UI;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public DialogueData dialogue;
    public Text dialogueContainer;
    public void TriggerDialue()
    {
        DialogueManager.Instance.StartDialogue(dialogue, dialogueContainer);
    }
}
