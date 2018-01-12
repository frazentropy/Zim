using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour {
	public int parentHeap;
	Heap heap;
	Board board;
	GameObject sparks;
	GameObject flames;
	public static bool AIDidTakeFirstTurn = false;

	void Awake() {
		GameObject.DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board> ();
		Heap[] hs = FindObjectsOfType<Heap>();
		foreach (Heap h in hs) {
			if (h.getHeapNum () == parentHeap) {
				heap = h;
			}
		}
		tag = "Unselected";
		if (FindObjectOfType<GameStateManager> ().getPlayersMode () == GameStateManager.PlayersMode.PvAI) {
			if (FindObjectOfType<GameStateManager> ().getPlayerTurn() == GameStateManager.PlayerTurn.SECOND_PLAYER) {
				Pawn[] pawns = FindObjectsOfType<Pawn>();
				if (!AIDidTakeFirstTurn) {
					if (FindObjectOfType<GameStateManager> ().getNumHeaps () == 5 && pawns.Length == 15) {
						FindObjectOfType<AIController> ().takeTurn ();
						AIDidTakeFirstTurn = true;
					} else if (FindObjectOfType<GameStateManager> ().getNumHeaps () == 6 && pawns.Length == 21) {
						FindObjectOfType<AIController> ().takeTurn ();
						AIDidTakeFirstTurn = true;
					} else if (FindObjectOfType<GameStateManager> ().getNumHeaps () == 7 && pawns.Length == 28) {
						FindObjectOfType<AIController> ().takeTurn ();
						AIDidTakeFirstTurn = true;
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((tag == "Selected") && (parentHeap != board.getSelectedHeap ())) {
			deselect ();
		}
	}

	public void select() {
		if (tag == "Selected") {
			return;
		};
		tag = "Selected";
		Board.numPawnsSelected++;
		//transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
		flames = Instantiate (Resources.Load ("Flames"), this.transform.position, Quaternion.identity) as GameObject;
		Vector3 rotation = new Vector3 (-90.0f, 0.0f, 0.0f);
		flames.transform.Rotate (rotation);
		GameObject.DontDestroyOnLoad (flames);
		GameObject fireSound = Instantiate(Resources.Load("Sound FX Player"), this.transform.position, Quaternion.identity) as GameObject;
	}

	public void deselect() {
		if (!(tag == "Selected")) {
			return;
		}
		tag = "Unselected";
		Board.numPawnsSelected--;
		//transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
		GameObject.Destroy (flames);
	}

	public void destroyPawn() {
		sparks = Instantiate (Resources.Load ("Sparks"), this.transform.position, Quaternion.identity) as GameObject;
		heap.removePawn();
		if (flames)
			GameObject.Destroy (flames);
		GameObject.Destroy (gameObject);
	}
}
