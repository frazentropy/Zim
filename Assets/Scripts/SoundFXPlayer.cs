using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXPlayer : MonoBehaviour {

	static SoundFXPlayer instance = null;
	void Awake() {
		Debug.Log("Sound FX Player Awake: " + GetInstanceID());
		if (instance) {
			Destroy(gameObject);
			print("Duplicate Sound FX Player " + GetInstanceID() + " self-destructing");
		} else {
			instance = this;
			this.transform.SetParent(null);
			GameObject.DontDestroyOnLoad(gameObject);
		}

		if (Mute.soundFXMuted) {
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		AudioSource fireSource = gameObject.AddComponent<AudioSource>();
		fireSource.clip = Resources.Load("fire") as AudioClip;
		fireSource.volume = 0.3f;
		fireSource.loop = true;
		GameObject.DontDestroyOnLoad (fireSource);
		fireSource.Play();
	}
	
	public void pawnDestroyEffect () {
		AudioClip clip = Resources.Load("PawnEffect") as AudioClip;
		AudioSource.PlayClipAtPoint(clip, new Vector3(0.0f, 0.0f, 0.0f));
	}

	void Update () {
		if (Mute.soundFXMuted) {
			Destroy(gameObject);
			Debug.Log ("Sound FX muted; destroying Sound FX Manager.");
		}

		if (GameObject.FindGameObjectsWithTag ("Selected").Length == 0) {
			Destroy (gameObject);
		}
	}
}
