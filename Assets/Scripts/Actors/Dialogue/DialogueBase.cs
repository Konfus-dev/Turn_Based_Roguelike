using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBase : MonoBehaviour
{
    private IEnumerator WriteText(string input, Text textContainer, float textSpeed)
    {
        foreach (char c in input)
        {
            textContainer.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
