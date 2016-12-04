using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public float turnDelay = .1f;
	public static GameManager instance = null;
	//public BoardManager boardScript;
	public int playerHealthPoints = 100;
	[HideInInspector] public bool playersTurn = true;

	private int level = 3;
	private List<Goblin> goblins;
	private bool goblinsMoving;

	// Use this for initialization
	void Awake () {
		
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);

		//boardScript = GetComponent<BoardManager>();
		InitGame();
	}

	void InitGame() {
		goblins.Clear ();
		//boardScript.SetupScene(level);
	}

	public void GameOver() {
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playersTurn || goblinsMoving)
			return;

		StartCoroutine (MoveEnemies ());
	}

	public void AddGoblinToList (Goblin script) {
		goblins.Add (script);
	}

	IEnumerator MoveEnemies() {
		goblinsMoving = true;
		yield return new WaitForSeconds (turnDelay);
		if (goblins.Count == 0) {
			yield return new WaitForSeconds (turnDelay);
		}

		for (int i = 0; i < goblins.Count; i++) {
			goblins [i].MoveEnemy ();
			yield return new WaitForSeconds (goblins [i].moveTime);
		}

		playersTurn = true;
		goblinsMoving = false;
	}
}
