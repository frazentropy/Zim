using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	void Awake() {
		Debug.Log ("Music player Awake: " + GetInstanceID ());
		if (instance) {
			Destroy (gameObject);
			print ("Duplicate music player " + GetInstanceID () + " self-destructing");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	bool muted;

	// Use this for initialization
	void Start () {
		muted = false;
		Debug.Log ("Music player Start: " + GetInstanceID ());
	}

	public void mute() {
		GetComponent<AudioSource>().mute = true;
		muted = true;
	}

	public void unmute() {
		GetComponent<AudioSource>().mute = false;
		muted = false;
		
	}

	public bool isMuted() {
		return muted;
	}
}
