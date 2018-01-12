using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawnDestroySound : MonoBehaviour {

	public static AudioClip pawnDestroy;
	static AudioSource audioSrcPawn;



	// Use this for initialization
	void Start () {

		pawnDestroy = Resources.Load<AudioClip> ("PawnEffect");
		audioSrcPawn = GetComponent<AudioSource> ();

		//fire = Resources.Load<AudioClip> ("fire");
		//audioSrcFire = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public static void PlaySound(string clip){

		switch (clip) {

		case "pawnDestroy":
			audioSrcPawn.PlayOneShot(pawnDestroy);
				break;
	
		}
	}
}
