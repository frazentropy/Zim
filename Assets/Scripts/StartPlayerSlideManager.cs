using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPlayerSlideManager : MonoBehaviour {
	public Slider playersSlider;
	public Slider startPlayerSlider;
	public Text handleText;
	GameStateManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameStateManager> ();
		startPlayerSlider.value = (float)(gameManager.getPlayerTurn ());
		StartPlayerValueChangeCheck ();
			
		startPlayerSlider.onValueChanged.AddListener (delegate {
			StartPlayerValueChangeCheck ();
		});

		playersSlider.onValueChanged.AddListener (delegate {
			PlayersValueChangeCheck ();
		});
	}

	public void StartPlayerValueChangeCheck() {
		if (startPlayerSlider.value == 0) {
			//Debug.Log ("Slider: Starting Player changed to Player 1");
			handleText.text = "Player 1 Starts";
			gameManager.setStartingPlayer (GameStateManager.PlayerTurn.FIRST_PLAYER);
		} else if (startPlayerSlider.value == 1) {
			if (playersSlider.value == 0) {
				//Debug.Log ("Slider: Starting Player Changed to Player 2");
				handleText.text = "Player 2 Starts";
			} else {
				//Debug.Log ("Slider: Starting Player Changed to AI");
				handleText.text = "AI Starts";
			}
			gameManager.setStartingPlayer (GameStateManager.PlayerTurn.SECOND_PLAYER);
		} else {
			Debug.Log ("Slider: ERROR Invalid starting player");
		}
	}

	public void PlayersValueChangeCheck() {
		if (startPlayerSlider.value == 1) {
			if (playersSlider.value == 0) {
				//Debug.Log ("Slider: Starting Player Changed to Player 2");
				handleText.text = "Player 2 Starts";
			} else {
				//Debug.Log ("Slider: Starting Player Changed to AI");
				handleText.text = "AI Starts";
			}
		}
	}
}
