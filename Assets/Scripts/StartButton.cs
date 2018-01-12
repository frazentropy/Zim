using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {
	GameStateManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameStateManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startGame() {
		gameManager.startGame ();
	}
}
