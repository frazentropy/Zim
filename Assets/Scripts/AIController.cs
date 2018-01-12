using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
	public bool isAIturn = false;

	static AIController instance = null;
	void Awake() {
		Debug.Log ("AI Controller Awake: " + GetInstanceID ());
		if (instance) {
			Destroy (gameObject);
			print ("Duplicate AI Controller " + GetInstanceID () + " self-destructing");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("AI Controller start.");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeTurn() {
		isAIturn = true;
		if (FindObjectOfType<GameStateManager> ().getAIDifficulty () == GameStateManager.AIDifficulty.EASY) {
			Debug.Log ("AI takes random move.");
			randomMove ();
		} else if (FindObjectOfType<GameStateManager> ().getAIDifficulty () == GameStateManager.AIDifficulty.MEDIUM) {
			int d2 = Random.Range (1, 2);
			if (d2 == 1) {
				Debug.Log ("AI takes correct (not random) move.");
				correctMove ();
			} else {
				Debug.Log ("AI takes random move.");
				randomMove ();
			}
		} else {
			Debug.Log ("AI takes correct (not random) move.");
			correctMove ();
		}
		isAIturn = false;
	}

	void correctMove() {
		int nimSumX = 0;
		Heap[] heapsArr = FindObjectsOfType<Heap>();
		List<Heap> heaps = new List<Heap> ();
		foreach (Heap h in heapsArr) {
			if (h.getNumPawns () > 0) {
				heaps.Add (h);
				nimSumX ^= h.getNumPawns ();
			}
		}
		if (nimSumX == 0) {
			Debug.Log ("AI: No correct move available; taking random move");
			randomMove ();
		} else {
			int selectedHeapNum = 1;
			int numPawnsToLeave = 0;
			int numPawnsToTake = 0;
			foreach (Heap h in heaps) {
				if ((nimSumX ^ h.getNumPawns ()) < h.getNumPawns ()) {
					selectedHeapNum = h.getHeapNum ();
					numPawnsToLeave = nimSumX ^ h.getNumPawns ();
					numPawnsToTake = h.getNumPawns () - numPawnsToLeave;
				}
			}
			Pawn[] pawns = FindObjectsOfType<Pawn> ();
			int i = numPawnsToLeave;
			foreach (Pawn p in pawns) {
				if ((p.parentHeap == selectedHeapNum) && (numPawnsToTake > 0)) {
					numPawnsToTake--;
					FindObjectOfType<SelectManager> ().selectObject (p.gameObject);
				}
			}
			if (FindObjectOfType<GameStateManager> ().getAISpeed () == GameStateManager.AISpeed.INSTANT) {
				FindObjectOfType<SelectManager> ().destroySelectedPawns ();
			} else {
				// Game Text Manager handles message and prompt text
			}
		}
	}

	void randomMove() {
		Heap[] heapsArr = FindObjectsOfType<Heap>();
		List<Heap> heaps = new List<Heap> ();
		foreach (Heap h in heapsArr) {
			if (h.getNumPawns () > 0) {
				heaps.Add (h);
			}
			Debug.Log("Heap " + h.getHeapNum() + " has " + h.getNumPawns() + " pawns.");
		}
		int selectedHeapIndex = Random.Range(1, heaps.Count);
		int numPawnsToRemove = Random.Range(1, heaps[selectedHeapIndex-1].getNumPawns());
		Debug.Log("AI removes " + numPawnsToRemove + " pawns from heap number " + heaps[selectedHeapIndex-1].getHeapNum());
		Pawn[] pawns = FindObjectsOfType<Pawn>();
		int pawnsRemaining = numPawnsToRemove;
		foreach (Pawn p in pawns) {
			if ((p.parentHeap == heaps[selectedHeapIndex-1].getHeapNum()) && (pawnsRemaining > 0)) {
				pawnsRemaining--;
				FindObjectOfType<SelectManager> ().selectObject (p.gameObject);
			}
		}

		if (FindObjectOfType<GameStateManager> ().getAISpeed () == GameStateManager.AISpeed.INSTANT) {
			FindObjectOfType<SelectManager> ().destroySelectedPawns ();
		} else {
			// Game Text Manager handles message and prompt text
		}
	}
}
