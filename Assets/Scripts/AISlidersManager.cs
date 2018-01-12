using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AISlidersManager : MonoBehaviour {
	public Slider speedSlider;
	public Text speedSliderText;
	public Slider difficultySlider;
	public Text difficultySliderText;
	GameStateManager gameManager;
	// Use this for initialization
	void Awake () {
		gameManager = FindObjectOfType<GameStateManager> ();
		if (gameManager.getPlayersMode () != GameStateManager.PlayersMode.PvAI) {
			GameObject.Destroy(speedSlider.GetComponentInChildren<Image>().gameObject);
			GameObject.Destroy(difficultySlider.GetComponentInChildren<Image>().gameObject);
			GameObject.Destroy(speedSlider.handleRect.GetComponent<Image>());
			GameObject.Destroy(difficultySlider.handleRect.GetComponent<Image>());
			speedSlider.enabled = false;
			difficultySlider.enabled = false;
			GameObject.Destroy(speedSlider);
			GameObject.Destroy(difficultySlider);
		}
	}
	void Start () {
		speedSlider.value = (float)gameManager.getAISpeed ();
		difficultySlider.value = (float)gameManager.getAIDifficulty ();
		ValueChangeCheck ();

		speedSlider.onValueChanged.AddListener (delegate {
			ValueChangeCheck ();
		});

		difficultySlider.onValueChanged.AddListener (delegate {
			ValueChangeCheck ();
		});
	}

	void ValueChangeCheck() {
		if (speedSlider.enabled) {
			if (speedSlider.value == 0) {
				speedSliderText.text = "AI Turns: Animated";
				gameManager.setAISpeed (GameStateManager.AISpeed.ANIMATED);
			} else if (speedSlider.value == 1) {
				speedSliderText.text = "AI Turns: Instant";
				gameManager.setAISpeed (GameStateManager.AISpeed.INSTANT);
			} else {
				Debug.Log ("Slider: ERROR Invalid AI speed");
			}

			if (difficultySlider.value == 0) {
				difficultySliderText.text = "Difficulty: Easy";
				gameManager.setAIDifficulty (GameStateManager.AIDifficulty.EASY);
			} else if (difficultySlider.value == 1) {
				difficultySliderText.text = "Difficulty: Medium";
				gameManager.setAIDifficulty (GameStateManager.AIDifficulty.MEDIUM);
			} else if (difficultySlider.value == 2) {
				difficultySliderText.text = "Difficulty: Hard";
				gameManager.setAIDifficulty (GameStateManager.AIDifficulty.HARD);
			} else {
				Debug.Log ("Slider: ERROR Invalid AI difficulty");
			}
		} else {
			difficultySliderText.text = " ";
			speedSliderText.text = " ";
		}

	}
	// Update is called once per frame
	void Update () {
		
	}
}
