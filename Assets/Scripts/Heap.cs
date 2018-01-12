using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap : MonoBehaviour {
	GameObject p;
	Board board;

	/* Heap Variables */
	int numPawns;
	int heapNum;

	List<GameObject> pawns;

	void Awake() {
		GameObject.DontDestroyOnLoad (gameObject);
		pawns = new List<GameObject> ();
		board = FindObjectOfType<Board> ();
		setHeapNum (board.getSelectedHeap ());
	}
	// Use this for initialization
	void Start () {
		setNumPawns (heapNum);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setNumPawns(int _numPawns) {
		numPawns = _numPawns;
		drawPawns ();
	}

	public int getNumPawns() {
		return pawns.Count;
	}

	public int getHeapNum() {
		return heapNum;
	}

	public void removePawn() {
		if (pawns.Count > 0)
			pawns.RemoveAt (0);
	}

	public void setHeapNum(int _heapNum) {
		heapNum = _heapNum;
		Debug.Log ("Heap number set: " + heapNum);
		
	}

	public void destroyHeap() {
		GameObject.Destroy (gameObject);
	}

	public void drawPawns() {
		float x = -((float)heapNum/2.4f)+0.5f;
		float y = 0;
		float z = 0;

		for (int i = 0; i < numPawns; i++) {
			Vector3 nextPos = new Vector3 (x, y, z);
			GameObject p = Instantiate (Resources.Load ("Sphere"), this.transform.position + nextPos, Quaternion.identity) as GameObject;
			p.GetComponent<Pawn> ().parentHeap = heapNum;
			pawns.Add (p);
			x += 0.8f;
		}
	}
}
