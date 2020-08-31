using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public DialogueData dialogue;

    public void TriggerDialue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
