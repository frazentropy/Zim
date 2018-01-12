using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameText : MonoBehaviour {
	public Text promptText;
	public Text messageText;
	GameStateManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameStateManager> ();
	}

	// Update is called once per frame
	void Update () {

		if (gameManager.getPlayersMode () == GameStateManager.PlayersMode.PvP) {
			
			if (gameManager.getPlayerTurn () == GameStateManager.PlayerTurn.FIRST_PLAYER) {
				messageText.text = "Player 1";
			} else {
				messageText.text = "Player 2";
			}

			if (Board.numPawnsSelected > 0) {
				promptText.text = "Remove " + Board.numPawnsSelected +
					((Board.numPawnsSelected == 1) ? " pawn" : " pawns");
			} else {
				promptText.text = "Select pawns";
			}

		} else if (gameManager.getPlayerTurn () == GameStateManager.PlayerTurn.FIRST_PLAYER) {
			

			messageText.text = "Player 1";
			if (Board.numPawnsSelected > 0) {
				promptText.text = "Remove " + Board.numPawnsSelected +
					((Board.numPawnsSelected == 1) ? " pawn" : " pawns");
			} else {
				promptText.text = "Select pawns";
			}

		} else {
			promptText.text = "Continue";
			int AIPawnsSelected = GameObject.FindGameObjectsWithTag ("Selected").Length;
			messageText.text = "AI selects " + AIPawnsSelected +
				((AIPawnsSelected == 1) ? " pawn." : " pawns.");
		}
	}
}
