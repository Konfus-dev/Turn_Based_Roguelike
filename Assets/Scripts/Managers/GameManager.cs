using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public float LevelStartDelay = 2f;
	public float turnDelay = 0.2f;
	public static GameManager Instance = null;
	public bool PlayersTurn = true;
	public bool NPCTurn = false;
	public bool initialScene = true;

	/*private Text LevelText;
	private GameObject LevelImage;
	private int Level = 1;*/
	private List<NPC> NPCs;  

	//Awake is always called before any Start functions
	void Awake()
	{
		if (Instance == null)
			Instance = this;

		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		NPCs = new List<NPC>();

	}


	//Update is called every frame.
	void Update()
	{
		if (PlayersTurn || NPCTurn /*|| doingSetup*/)
			return;

		StartCoroutine(MoveNPCs());
	}

	public void AddNPCToList(NPC script)
	{
		NPCs.Add(script);
	}

	public void RemoveNPCFromList(NPC script)
	{
		NPCs.Remove(script);
	}
	public void ClearNPCs()
	{
		NPCs.Clear();
	}

	public void GameOver()
	{
		//levelText.text = "Game Over";

		//levelImage.SetActive(true);

		enabled = false;
	}

	IEnumerator MoveNPCs()
	{

		NPCTurn = true;

		yield return new WaitForSeconds(turnDelay);

		if (NPCs.Count == 0)
		{
			yield return new WaitForSeconds(turnDelay);
		}

		for (int i = 0; i < NPCs.Count; i++)
		{
			NPCs[i].movement.myNode = NPCs[i].movement.grid.NodeFromPosition(NPCs[i].transform.position);
			//Debug.Log("enemy moving " + Enemies[i].travelTurns + " time(s)");
			for (int j = 0; j < NPCs[i].travelTurns; j++)
			{
				if (NPCs[i].movement.MoveNPC())
				{


				}
				else
				{

				}
			}

			yield return new WaitForSeconds(NPCs[i].moveTime);
		}

		PlayersTurn = true;

		NPCTurn = false;
	}
}


