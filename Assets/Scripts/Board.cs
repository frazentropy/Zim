using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour {
	static Board instance = null;
	void Awake() {
		Debug.Log ("Board Awake: " + GetInstanceID ());
		if (instance) {
			Destroy (gameObject);
			print ("Duplicate Board " + GetInstanceID () + " self-destructing");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}

		heaps = new List<Heap>();
	}

	GameStateManager gameState;

	/* Board Variables */
	public static int numPawnsSelected = 0;
	List<Heap> heaps;
	int numHeaps;
	int selectedHeap;

	void Start () {
		selectedHeap = 0;
		Debug.Log ("Board Start: " + GetInstanceID ());
		gameState = FindObjectOfType<GameStateManager> ();
		numHeaps = gameState.getNumHeaps ();
		Debug.Log ("Board initialized with " + numHeaps + " heaps.");
		float x = 0;
		float y = -numHeaps/2 + 1.0f;
		float z = 0;
		for (int i = 0; i < numHeaps; i++) {
			selectedHeap = i + 1;
			Heap h = Instantiate(Resources.Load("Heap"), new Vector3(x, y, z), Quaternion.identity) as Heap;
			heaps.Add(h);

			y += 0.8f;
			//heaps [i].setHeapNum (i + 1);
		}
	}

	public int getSelectedHeap() {
		return selectedHeap;
	}

	public void selectHeap(int _selectedHeap) {
		selectedHeap = _selectedHeap;
	}

	public bool winCheck() {
		int pawnsRemaining = 0;
		Heap[] hs = FindObjectsOfType<Heap> ();
		foreach (Heap h in hs) {
			pawnsRemaining += h.getNumPawns ();
		}
		Debug.Log ("Pawns remaining: " + pawnsRemaining);
			
		if (gameState.getGameMode () == GameStateManager.GameMode.MISERE) {
			// Misere Mode
			if (pawnsRemaining == 1) {
				gameState.win (gameState.getPlayerTurn ());
				return true;
			} else if (pawnsRemaining < 1) {
				gameState.win (gameState.getNextPlayerTurn ());
				return true;
			} else {
				return false;
			}

		} else {
			// Normal Mode
			if (pawnsRemaining == 0) {
				gameState.win (gameState.getPlayerTurn ());
				return true;
			} else {
				return false;
			}
		}
	}

	public void destroyBoard () {
		GameObject.Destroy (gameObject);
	}
}
