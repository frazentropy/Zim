using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
	AIController aiController;
	//LevelManager levelManager;
	/* Game State Types */
	public enum PlayersMode {
		PvP,
		PvAI
	};
	public enum GameMode {
		NORMAL,
		MISERE
	};
	public enum GameState {
		START,
		IN_PROGRESS,
		OVER
	};
	public enum PlayerTurn {
		FIRST_PLAYER,
		SECOND_PLAYER
	}
	public enum AISpeed {
		ANIMATED,
		INSTANT
	}
	public enum AIDifficulty {
		EASY,
		MEDIUM,
		HARD
	}


	/* Game State Variables */
	//Board board;
	PlayersMode playersMode;
	GameMode gameMode;
	GameState gameState;
	PlayerTurn playerTurn;
	PlayerTurn startingPlayer;
	PlayerTurn winningPlayer;
	AISpeed aiSpeed;
	AIDifficulty aiDifficulty;
	int numHeaps;



	static GameStateManager instance = null;
	void Awake() {
		Debug.Log ("Game state manager Awake: " + GetInstanceID ());
		if (instance) {
			Destroy (gameObject);
			print ("Duplicate game state manager " + GetInstanceID () + " self-destructing");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}
		
	// Use this for initialization
	void Start () {
		//levelManager = FindObjectOfType<LevelManager> ();
		Debug.Log ("Game state manager Start: " + GetInstanceID ());
		setPlayersMode (PlayersMode.PvP);
		setPlayerTurn (PlayerTurn.FIRST_PLAYER);
		setGameMode (GameMode.NORMAL);
		setGameState (GameState.START);
		setAISpeed (AISpeed.ANIMATED);
		setAIDifficulty (AIDifficulty.MEDIUM);
		Debug.Log ("Game Manager: Game state changed to START");
		setNumHeaps (5);

	}

	#region SETTERS
	public void setPlayersMode(PlayersMode _playersMode) {
		playersMode = _playersMode;
		Debug.Log ("Game Manager: Players mode set to " + playersMode.ToString());
	}

	public void setGameMode(GameMode _gameMode) {
		gameMode = _gameMode;
		Debug.Log ("Game Manager: Game mode set to " + gameMode.ToString());
	}

	public void setGameState(GameState _gameState) {
		gameState = _gameState;
		Debug.Log ("Game Manager: Game state set to " + gameState.ToString());
	}

	public void setStartingPlayer(PlayerTurn _startingPlayer) {
		startingPlayer = _startingPlayer;
		setPlayerTurn (startingPlayer);
	}

	public void setPlayerTurn(PlayerTurn _playerTurn) {
		playerTurn = _playerTurn;
		Debug.Log ("Game Manager: Player turn set to " + playerTurn.ToString());
	}

	public void setNumHeaps(int _numHeaps) {
		numHeaps = _numHeaps;
		Debug.Log ("Game Manager: Num heaps set to " + numHeaps);
	}

	public void setAISpeed (AISpeed _aiSpeed) {
		aiSpeed = _aiSpeed;
	}

	public void setAIDifficulty (AIDifficulty _aiDifficulty) {
		aiDifficulty = _aiDifficulty;
	}
	#endregion

	#region GETTERS
	public PlayersMode getPlayersMode() {
		return playersMode;
	}

	public GameMode getGameMode() {
		return gameMode;
	}

	public GameState getGameState() {
		return gameState;
	}

	public PlayerTurn getStartingPlayer() {
		return startingPlayer;
	}

	public PlayerTurn getPlayerTurn() {
		return playerTurn;
	}

	public PlayerTurn getNextPlayerTurn() {
		if (playerTurn == PlayerTurn.FIRST_PLAYER)
			return PlayerTurn.SECOND_PLAYER;
		else
			return PlayerTurn.FIRST_PLAYER;
	}

	public PlayerTurn getWinningPlayer() {
		return winningPlayer;
	}

	public int getNumHeaps() {
		return numHeaps;
	}

	public AISpeed getAISpeed() {
		return aiSpeed;
	}

	public AIDifficulty getAIDifficulty() {
		return aiDifficulty;
	}
	#endregion

	public void startGame() {
		setGameState (GameState.IN_PROGRESS);
		LevelManager lm = FindObjectOfType<LevelManager> ();
		if (lm)
			lm.LoadLevel ("Game");
		else
			Debug.LogError ("ERROR: No level manager");
		if (playersMode == PlayersMode.PvAI) {
			aiController = Instantiate(Resources.Load("AI Controller"), new Vector3(0, 0, 0), Quaternion.identity) as AIController;
			if (startingPlayer == PlayerTurn.SECOND_PLAYER) {
				Debug.Log ("AI takes first turn");
				//aiController.takeTurn ();
			}
		}
	}

	public void nextTurn () {
		if (playerTurn == PlayerTurn.FIRST_PLAYER) {
			playerTurn = PlayerTurn.SECOND_PLAYER;
			if (playersMode == PlayersMode.PvAI) {
				aiController = FindObjectOfType<AIController> ();
				if (!aiController)
					Debug.LogError ("nextTurn: Can't find AI Controller");
				aiController.takeTurn ();
			} else {
				// TODO take Player 2 turn
			}
		}
		else
			playerTurn = PlayerTurn.FIRST_PLAYER;
	}

	public void win(PlayerTurn player) {
		winningPlayer = player;
		// Destroy what GameStateManager doesn't need; destroy the rest at Win/Loss screen.
		Pawn[] ps = FindObjectsOfType<Pawn>();
		foreach (Pawn p in ps)
			p.destroyPawn();
		
		Heap[] hs = FindObjectsOfType<Heap>();
		foreach (Heap h in hs)
			h.destroyHeap();

		Board board = FindObjectOfType<Board> ();
		board.destroyBoard();

		Debug.Log (winningPlayer + " won the game.");

		LevelManager lm = FindObjectOfType<LevelManager> ();
		lm.LoadLevel ("End");

	}

	public void destroyGameStateManager() {
		GameObject.Destroy (gameObject);
	}
		
}
