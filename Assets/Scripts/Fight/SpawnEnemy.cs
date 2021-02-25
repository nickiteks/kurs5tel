using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{
	[SerializeField]
	private GameObject enemyEncounterPrefab;
	[SerializeField]
	private GameObject party;

	private Inventory inventory;
	private DatabaseInventory database;

	private bool spawning = false;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(party.gameObject);
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name != "FightScene")
		{
			if (this.spawning)
			{
				inventory = FindObjectOfType<Inventory>();
				database = FindObjectOfType<DatabaseInventory>();

				
			}
			SceneManager.sceneLoaded -= OnSceneLoaded;
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			this.spawning = true;
			Time.timeScale = 0;
			SceneManager.LoadScene("FightScene");
		}
	}
}
