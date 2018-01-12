using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour {
	GameStateManager gameManager;

	public void restart () {
		gameManager = FindObjectOfType<GameStateManager>();
		gameManager.destroyGameStateManager();
		AIController ai = FindObjectOfType <AIController> ();
		if (ai) {
			GameObject.Destroy (ai.gameObject);
		}
		LevelManager lm = FindObjectOfType <LevelManager>();
		lm.LoadLevel("Start");
	}
}
