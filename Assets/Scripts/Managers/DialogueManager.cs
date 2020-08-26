using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance = null;

	private Queue<string> lines;

	void Awake()
	{
		lines = new Queue<string>();

		if (Instance == null)
			Instance = this;

		else if (Instance != this)
			Destroy(gameObject);
	}

	public void StartDialogue(DialogueData dialogue, Text textContainer)
    {
		foreach(string line in dialogue.lines)
		{
			this.lines.Enqueue(line);
        }

		DisplayNextLine();
    }

	private void DisplayNextLine()
    {
		if(lines.Count == 0)
        {
			EndDialogue();
			return;
        }

		string line = lines.Dequeue();
    }

	private void EndDialogue()
    {

    }

	private IEnumerator WriteText(string input, Text textContainer, float textSpeed)
	{
		foreach (char c in input)
		{
			textContainer.text += c;
			yield return new WaitForSeconds(textSpeed);
		}
	}

}
