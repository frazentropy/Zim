using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlideManager : MonoBehaviour {
	public Slider slider;
	public Text handleText;
	MusicPlayer music;

	// Use this for initialization
	void Start () {
		music = FindObjectOfType<MusicPlayer> ();
		if (music.isMuted ()) {
			slider.value = 0;
		} else {
			slider.value = 1;
		}
		ValueChangeCheck ();
			
		slider.onValueChanged.AddListener (delegate {
			ValueChangeCheck ();
		});
	}

	public void ValueChangeCheck() {
		if (slider.value == 0) {
			//Debug.Log ("Slider: Music OFF");
			handleText.text = "Music OFF";
			music.mute ();
		} else if (slider.value == 1) {
			//Debug.Log ("Slider: Music ON");
			handleText.text = "Music ON";
			music.unmute ();
		} else {
			Debug.Log ("Slider: ERROR Invalid value");
		}
	}
}
