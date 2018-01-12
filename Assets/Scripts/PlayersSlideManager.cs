using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersSlideManager : MonoBehaviour {
	public Slider slider;
	public Text handleText;
	GameStateManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameStateManager> ();
		slider.value = (float)(gameManager.getPlayersMode ());
		ValueChangeCheck ();
			
		slider.onValueChanged.AddListener (delegate {
			ValueChangeCheck ();
		});
	}

	public void ValueChangeCheck() {
		if (slider.value == 0) {
			//Debug.Log ("Slider: Player mode changed to Player vs. Player");
			handleText.text = "Player vs. Player";
			gameManager.setPlayersMode (GameStateManager.PlayersMode.PvP);
		} else if (slider.value == 1) {
			//Debug.Log ("Slider: Player mode changed to Player vs. AI");
			handleText.text = "Player vs. AI";
			gameManager.setPlayersMode (GameStateManager.PlayersMode.PvAI);
		} else {
			Debug.Log ("Slider: ERROR Invalid player mode");
		}
	}
}
