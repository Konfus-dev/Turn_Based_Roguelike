using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public float LevelStartDelay = 2f;
	public float turnDelay = 0.2f;
	public static GameManager Instance = null;
	public bool PlayersTurn = true;
	public bool EnemiesTurn = false;

	private Text LevelText;
	private GameObject LevelImage;
	private int Level = 1;
	private List<Enemy> Enemies;  

	//Awake is always called before any Start functions
	void Awake()
	{
		if (Instance == null)
			Instance = this;

		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		Enemies = new List<Enemy>();
	}

	//Update is called every frame.
	void Update()
	{
		if (PlayersTurn || EnemiesTurn /*|| doingSetup*/)
			return;

		StartCoroutine(MoveEnemies());
	}

	public void AddEnemyToList(Enemy script)
	{
		Enemies.Add(script);
	}

	public void GameOver()
	{
		//levelText.text = "Game Over";

		//levelImage.SetActive(true);

		enabled = false;
	}

	IEnumerator MoveEnemies()
	{
		Debug.Log("enemies turn");

		EnemiesTurn = true;

		yield return new WaitForSeconds(turnDelay);

		if (Enemies.Count == 0)
		{
			yield return new WaitForSeconds(turnDelay);
		}

		for (int i = 0; i < Enemies.Count; i++)
		{
			Debug.Log("moving enemy " + i);
			Enemies[i].MoveEnemy();

			yield return new WaitForSeconds(Enemies[i].MoveTime);
		}

		PlayersTurn = true;

		EnemiesTurn = false;
	}
}


