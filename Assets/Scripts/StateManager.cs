using System.Collections; using System.Collections.Generic; using UnityEngine;  public class StateManager : MonoBehaviour {  	// Use this for initialization 	void Awake () {         stepNumber = 0;
        /* If player number decided, leave this         playerPieces = GameObject.FindObjectsOfType<PlayerPiece>();         playerTiles = new int[playerPieces.Length];         for (int i = 0; i < playerTiles.Length; i++) {
            playerTiles[i] = 0;
        }         */

        SnakesAndLadders = new int[100];         for (int i = 0; i < SnakesAndLadders.Length; i++) {
            SnakesAndLadders[i] = -1;
        }         SetSnakesAndLadders();          mission = GameObject.FindObjectOfType<Mission>();
        challenge = GameObject.FindObjectOfType<Challenge>();
        playerNumber = GameObject.FindObjectOfType<PlayerNumber>();
        move = GameObject.FindObjectOfType<Move>();
        win = GameObject.FindObjectOfType<Win>();
    }      public int stepNumber;     public GameObject[] tiles;     PlayerPiece[] playerPieces;     int[] playerTiles;     public int currentPlayer = 0;     int challengedPlayer = -1;     public bool gameWon = false;     public bool missionStatus = true;     public bool challengeStatus = true;      private int[] SnakesAndLadders;      Mission mission;     Challenge challenge;     PlayerNumber playerNumber;     Move move;     Win win;      public enum TurnPhase {         CALL_PLAYERNUMBER,         WAITING_FOR_PLAYER_NUMBER,         DECIDE_PLAYER_NUMBER,         ROLL,         WAITING_FOR_ROLL,         START_MOVING,         WAITING_FOR_ANIMATION,         DONE_ANIMATION,         CHECK_FOR_SNAKES_AND_LADDERS,         CHECK_FOR_CHALLENGE,         WAITING_FOR_CHALLENGE_COMPLETED,         CHALLENGE_COMPLETED,         WAITING_FOR_MISSION_COMPLETED,         MISSION_COMPLETED,         TURN_FINISH,         GAME_OVER,         WAITING_FOR_START_OVER     };     public TurnPhase currentPhase = TurnPhase.CALL_PLAYERNUMBER;  	// Update is called once per frame 	void Update () {         switch (currentPhase) {             case TurnPhase.CALL_PLAYERNUMBER:                 playerNumber.PlayerNumberWindow();                 currentPhase = TurnPhase.WAITING_FOR_PLAYER_NUMBER;                 break;              case TurnPhase.DECIDE_PLAYER_NUMBER:                 playerPieces = GameObject.FindObjectsOfType<PlayerPiece>();
                playerTiles = new int[playerPieces.Length];
                for (int i = 0; i < playerTiles.Length; i++) {
                    playerTiles[i] = 0;
                }                 currentPhase = TurnPhase.ROLL;                 break;              case TurnPhase.ROLL:
                // enable the number buttons
                move.MoveWindow();                 currentPhase = TurnPhase.WAITING_FOR_ROLL;                 break;              case TurnPhase.START_MOVING:                 // call the player MoveForward(stepNumber) to move                 currentPhase = TurnPhase.WAITING_FOR_ANIMATION;                 playerTiles[currentPlayer] = playerPieces[currentPlayer].MoveForward(stepNumber);                 break;              case TurnPhase.DONE_ANIMATION:                 // check for winning condition                 // if true, goto GAME_OVER                 if (gameWon == true) {                     currentPhase = TurnPhase.GAME_OVER;                 } else {
                    // else goto CHECK_FOR_SNAKES_AND_LADDERS
                    currentPhase = TurnPhase.CHECK_FOR_SNAKES_AND_LADDERS;                 }                 break;              case TurnPhase.CHECK_FOR_SNAKES_AND_LADDERS:
                // check for snakes and ladders from map/array
                // if key exists, call playerPiece MoveTo, WAITING_FOR_ANIMATION
                if (SnakesAndLadders[playerTiles[currentPlayer]] != -1) {
                    currentPhase = TurnPhase.WAITING_FOR_ANIMATION;
                    playerTiles[currentPlayer] = playerPieces[currentPlayer].MoveTo(SnakesAndLadders[playerTiles[currentPlayer]]);
                } else {
                    // else go to CHECK_FOR_CHALLENGE
                    currentPhase = TurnPhase.CHECK_FOR_CHALLENGE;
                }                 break;              case TurnPhase.CHECK_FOR_CHALLENGE:
                // TODO: May want to change the challenge rules
                bool challenged = false;
                for (int i = 0; i < playerTiles.Length; i++) {
                    if (i != currentPlayer && 
                        playerTiles[currentPlayer] == playerTiles[i]) {
                        challenged = true;
                        challengedPlayer = i;
                        break;
                    }
                }
                // if landed onto tiles occupied, activate CHALLENGE, goto WAITING_FOR_CHALLENGED_COMPLETED
                if (challenged) {
                    challenge.ChallengeWindow();
                    currentPhase = TurnPhase.WAITING_FOR_CHALLENGE_COMPLETED;
                } else {
                    // Activate MISSION
                    mission.MissionWindow();
                    currentPhase = TurnPhase.WAITING_FOR_MISSION_COMPLETED;
                }                 break;              case TurnPhase.CHALLENGE_COMPLETED:
                currentPhase = TurnPhase.WAITING_FOR_ANIMATION;
                if (challengeStatus) {
                    playerTiles[challengedPlayer] = playerPieces[challengedPlayer].MoveBackward(1);
                } else {
                    playerTiles[currentPlayer] = playerPieces[currentPlayer].MoveBackward(1);
                }                 break;              case TurnPhase.MISSION_COMPLETED:                 if (!missionStatus) {
                    // move backward
                    currentPhase = TurnPhase.WAITING_FOR_ANIMATION;
                    playerTiles[currentPlayer] = playerPieces[currentPlayer].MoveBackward(1);
                } else {
                    currentPhase = TurnPhase.TURN_FINISH;
                }                 break;              case TurnPhase.TURN_FINISH:                 // Mission complete or failed                 // Change current player to next player.                 currentPlayer = (currentPlayer + 1) % playerPieces.Length;                 currentPhase = TurnPhase.ROLL;                 break;              case TurnPhase.GAME_OVER:
                // Activate GAME_OVER text.
                win.WinWindow();                 // Waiting for player to start over                 currentPhase = TurnPhase.WAITING_FOR_START_OVER;                 break;              case TurnPhase.WAITING_FOR_PLAYER_NUMBER:             case TurnPhase.WAITING_FOR_ROLL:             case TurnPhase.WAITING_FOR_ANIMATION:             case TurnPhase.WAITING_FOR_MISSION_COMPLETED:             case TurnPhase.WAITING_FOR_START_OVER:                 break;         } 	}      // Set all the snakes and ladders tiles connections     void SetSnakesAndLadders () {
        SnakesAndLadders[7] = 32;
        SnakesAndLadders[16] = 5;
        SnakesAndLadders[18] = 24;
        SnakesAndLadders[20] = 42;
        SnakesAndLadders[22] = 3;
        SnakesAndLadders[36] = 55;
        SnakesAndLadders[44] = 78;
        SnakesAndLadders[48] = 13;
        SnakesAndLadders[50] = 90;
        SnakesAndLadders[58] = 79;
        SnakesAndLadders[62] = 40;
        SnakesAndLadders[63] = 80;
        SnakesAndLadders[65] = 88;
        SnakesAndLadders[68] = 30;
        SnakesAndLadders[70] = 94;
        SnakesAndLadders[74] = 96;
        SnakesAndLadders[76] = 53;
        SnakesAndLadders[82] = 14;
        SnakesAndLadders[93] = 34;
    } } 