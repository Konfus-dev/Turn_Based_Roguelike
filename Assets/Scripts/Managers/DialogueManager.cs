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

	public void StartDialogue(Dialogue dialogue, Text textContainer, float textSpeed)
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
