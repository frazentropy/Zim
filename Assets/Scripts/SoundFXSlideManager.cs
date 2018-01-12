using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundFXSlideManager : MonoBehaviour {
	public Slider slider;
	public Text handleText;

	// Use this for initialization
	void Start () {
		if (Mute.soundFXMuted) {
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
			Debug.Log ("Slider: Sound FX OFF");
			handleText.text = "Sound FX OFF";
			Mute.soundFXMuted = true;
		} else if (slider.value == 1) {
			Debug.Log ("Slider: Sound FX ON");
			handleText.text = "Sound FX ON";
			Mute.soundFXMuted = false;
			if (GameObject.FindGameObjectsWithTag ("Selected") != null) {
				GameObject fireSound = Instantiate(Resources.Load("Sound FX Player"), this.transform.position, Quaternion.identity) as GameObject;
			}
		} else {
			Debug.Log ("Slider: ERROR Invalid value");
		}
	}
}
