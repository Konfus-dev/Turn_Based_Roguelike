using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance = null;

	public Text nameContainer;
	public Text dialogueContainer;

	public Color[] colors;
	public char[] colorKeys;

	public Font[] fonts;
	public char[] fontKeys;

	public float[] speeds;
	public char[] speedKeys;

	public float[] sizes;
	public char[] sizeKeys;

	private Queue<string> lines;

	private Dictionary<char, Color> colorDictionary;
	private Dictionary<char, Font> fontDictionary;
	private Dictionary<char, Font> speedDictionary;
	private Dictionary<char, Font> sizeDictionary;


	void Awake()
	{
		lines = new Queue<string>();
		colorDictionary = new Dictionary<char, Color>();
		fontDictionary = new Dictionary<char, Font>();

		for(int c = 0; c < colors.Length; c++)
        {
			colorDictionary.Add(colorKeys[c], colors[c]);
		}
		for (int f = 0; f < fonts.Length; f++)
		{
			fontDictionary.Add(fontKeys[f], fonts[f]);
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
				if (colorDictionary.ContainsKey(c))
                {

                }
				if (fontDictionary.ContainsKey(c))
                {

                }
				if (speedDictionary.ContainsKey(c))
                {

                }
                if (sizeDictionary.ContainsKey(c))
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

}
