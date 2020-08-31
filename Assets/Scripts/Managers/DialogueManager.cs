using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance = null;

	public Text nameContainer;
	public Text dialogueContainer;

	public Font[] fonts;
	public char[] fontKeys;

	public float[] speeds;
	public char[] speedKeys;

	private Queue<string> lines;

	private Dictionary<char, Font> fontDictionary;
	private Dictionary<char, float> speedDictionary;


	void Awake()
	{
		lines = new Queue<string>();
		fontDictionary = new Dictionary<char, Font>();
		speedDictionary = new Dictionary<char, float>();

		for (int f = 0; f < fonts.Length; f++)
		{
			fontDictionary.Add(fontKeys[f], fonts[f]);
		}

		for (int s = 0; s < speeds.Length; s++)
		{
			speedDictionary.Add(speedKeys[s], speeds[s]);
		}

		if (Instance == null)
			Instance = this;

		else if (Instance != this)
			Destroy(gameObject);
	}

	public void StartDialogue(DialogueData dialogueData)
    {
		lines.Clear();

		nameContainer.text = dialogueData.name;

		nameContainer.enabled = true;
		dialogueContainer.enabled = true;

		foreach (string line in dialogueData.lines)
		{
			this.lines.Enqueue(line);
        }

		DisplayNextLine(dialogueData.talkSpeed, dialogueData.voice);
    }

	private void DisplayNextLine(float textDisplaySpeed, AudioClip audio)
    {
		if(lines.Count == 0)
        {
			EndDialogue();
			return;
        }

		string line = lines.Dequeue();

		StopAllCoroutines();
		StartCoroutine(WriteText(line, textDisplaySpeed, audio));

	}

	private void EndDialogue()
    {
		nameContainer.enabled = false;
		dialogueContainer.enabled = false;
	}

	private IEnumerator WriteText(string input, float textSpeed, AudioClip audio)
	{
		bool parse = false;
		foreach (char c in input)
		{
			if(c == '<')
            {
				parse = true;
				continue;
            }
			else if(parse)
            {
				//TODO: finish functionality to change text color, size, speed and font with a marker ex: <r> hello 
				if (c == '!')
				{
					ResetDialogueVisuals();
					continue;
				}

				if (fontDictionary.ContainsKey(c))
                {

                }
				if (speedDictionary.ContainsKey(c))
                {

                }
				continue;
			}
			else if(c =='>')
            {
				parse = false;
				continue;
            }

			SoundManager.Instance.PlaySound(audio);

			dialogueContainer.text += c;

			yield return new WaitForSeconds(textSpeed);
		}
	}

	private void ResetDialogueVisuals()
    {

    }

}
