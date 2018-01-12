using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour {
	public GameObject selectedObject;
	Board board;
	GameStateManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameStateManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		if (Physics.Raycast (ray, out hitInfo)) {
			GameObject hitObject = hitInfo.transform.root.gameObject;
			//Debug.Log ("Mouse is over: " + hitObject.name);
			if (Input.GetMouseButtonDown (0)) {
				if (gameManager.getPlayersMode () == GameStateManager.PlayersMode.PvAI && gameManager.getPlayerTurn () == GameStateManager.PlayerTurn.SECOND_PLAYER) {
					// Do nothing on AI turn
				} else {
					// Select object from raycast
					selectObject (hitObject);
				}
			}
		}
	}

	public void selectObject(GameObject obj) {
		if (obj.GetComponentInChildren<Pawn>().tag == "Selected") {
			obj.GetComponentInChildren<Pawn> ().deselect ();
			//ClearSelection (obj);
		} else {
			selectHeap (obj);
			obj.GetComponentInChildren<Pawn> ().select ();
		}
	}

	void selectHeap(GameObject obj) {
		board = FindObjectOfType<Board> ();
		int currentHeap = board.getSelectedHeap ();
		int nextHeap = obj.GetComponentInChildren<Pawn> ().parentHeap;
		if (currentHeap != nextHeap) {
			GameObject[] selectedObjects = GameObject.FindGameObjectsWithTag("Selected");
			foreach (GameObject so in selectedObjects) {
				so.GetComponentInChildren<Pawn> ().deselect ();
			}
			board.selectHeap (nextHeap);
		}
	}

	public void destroySelectedPawns() {
		SoundFXPlayer sfx = FindObjectOfType<SoundFXPlayer>();
		if (sfx)
			sfx.pawnDestroyEffect();
		GameObject[] selectedObjects = GameObject.FindGameObjectsWithTag("Selected");
		if (selectedObjects.Length > 0) {
			foreach (GameObject so in selectedObjects) {
				so.GetComponentInChildren<Pawn> ().destroyPawn ();
			}
			Board.numPawnsSelected = 0;
			board = FindObjectOfType<Board> ();
			if (!board.winCheck ()) {
				gameManager.nextTurn ();
			}
		}
	}
}
