using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour {
	public Text winText;
	public Text winMessage;
	GameStateManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameStateManager> ();
		if (gameManager.getWinningPlayer () == GameStateManager.PlayerTurn.FIRST_PLAYER) {
			// First player won
			if (gameManager.getPlayersMode () == GameStateManager.PlayersMode.PvP) {
				// PvP
				winText.text = "Player 1 Wins!";
				if (gameManager.getGameMode () == GameStateManager.GameMode.MISERE) {
					// Mode == MISERE
					winMessage.text = "Player 2 was left with the last pawn.";
				} else {
					// Mode == NORMAL
					winMessage.text = "Player 1 took the last pawn.";
				}
			} else {
				// PvAI
				winText.text = "You win!";
				if (gameManager.getGameMode () == GameStateManager.GameMode.MISERE) {
					// Mode == MISERE
					winMessage.text = "AI was left with the last pawn.";
				} else {
					// Mode == NORMAL
					winMessage.text = "You took the last pawn.";
				}
			}
		} else {
			// Second player won
			if (gameManager.getPlayersMode () == GameStateManager.PlayersMode.PvP) {
				// PvP
				winText.text = "Player 2 Wins!";
				if (gameManager.getGameMode () == GameStateManager.GameMode.MISERE) {
					// Mode == MISERE
					winMessage.text = "Player 1 was left with the last pawn.";
				} else {
					// Mode == NORMAL
					winMessage.text = "Player 2 took the last pawn.";
				}
			} else {
				// PvAI
				winText.text = "AI Wins!";
				if (gameManager.getGameMode () == GameStateManager.GameMode.MISERE) {
					// Mode == MISERE
					winMessage.text = "You were left with the last pawn.";
				} else {
					// Mode == NORMAL
					winMessage.text = "AI took the last pawn.";
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
