using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	GameStateManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameStateManager> ();
		Debug.Log ("Level manager Start: " + GetInstanceID ());
	}

	public void LoadLevel(string name) {
		Debug.Log ("GameState = " + gameManager.getGameState().ToString() + ", Level load requested for: " + name);
		SceneManager.LoadScene (name);
	}

	public void Back() {
		if (gameManager.getGameState () == GameStateManager.GameState.START) {
			LoadLevel ("Start");
		} else {
			LoadLevel ("Game");
		}
	}
}
