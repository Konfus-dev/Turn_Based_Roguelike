using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public DialogueData dialogue;

    //look into this before writing dialogue, you can change text color, size, etc. by using certian flags ex: <color=green>hi</color=green>
    //https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html

    public void TriggerDialue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
