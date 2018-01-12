using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeapSlideManager : MonoBehaviour {
	public Slider slider;
	public Text handleText;
	GameStateManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameStateManager> ();
		int numHeaps = gameManager.getNumHeaps ();
		slider.value = (float)numHeaps;
		ValueChangeCheck ();
		slider.onValueChanged.AddListener (delegate {
			ValueChangeCheck ();
		});
	}

	public void ValueChangeCheck() {
		//Debug.Log ("Slider: numHeaps changed to " + slider.value);
		handleText.text = slider.value.ToString ();
		gameManager.setNumHeaps ((int)(slider.value));
		//handleText.alignment = TextAnchor.MiddleCenter;
	}
}
