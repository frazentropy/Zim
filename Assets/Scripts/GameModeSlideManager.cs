using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSlideManager : MonoBehaviour {
	public Slider slider;
	public Text handleText;
	GameStateManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameStateManager> ();
		slider.value = (float)(gameManager.getGameMode ());
		ValueChangeCheck ();
			
		slider.onValueChanged.AddListener (delegate {
			ValueChangeCheck ();
		});
	}

	public void ValueChangeCheck() {
		if (slider.value == 0) {
			//Debug.Log ("Slider: Game mode changed to Normal");
			handleText.text = "Normal Mode";
			gameManager.setGameMode (GameStateManager.GameMode.NORMAL);
		} else if (slider.value == 1) {
			//Debug.Log ("Slider: Game mode changed to Misère");
			handleText.text = "Misère Mode";
			gameManager.setGameMode (GameStateManager.GameMode.MISERE);
		} else {
			Debug.Log ("Slider: ERROR Invalid player mode");
		}
	}
}
