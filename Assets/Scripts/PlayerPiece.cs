using System.Collections; using System.Collections.Generic; using UnityEngine;  public class PlayerPiece : MonoBehaviour {  	// Use this for initialization 	void Start () {         theStateManager = GameObject.FindObjectOfType<StateManager>();         finalPosition = this.transform.position;         finalTile = 0;     }      StateManager theStateManager;     Vector3 finalPosition;     int finalTile;      Vector3[] moveQueue;     // Vector3 velocity;     // float smoothTime = 0.2f;     int speed = 2;     float smoothDistance = 0.01f;     int steps = 0;     int moveQueueIndex = 0;     public bool isAnimating = false;      enum Direction {
        FORWARD,
        BACKWARD,
        TO
    };     Direction dir;      // Update is called once per frame     void Update () {         if (isAnimating == false || moveQueue == null) return;  		if (Vector3.Distance(finalPosition, this.transform.position) < smoothDistance) {             // Reached the next tile             if (steps != 0) {                 // Not yet reach the final tile                 finalPosition = moveQueue[moveQueueIndex - steps];                 steps--;             } else {
                // Reached final tile, Done animation                 switch (dir) {
                    case Direction.FORWARD:
                        theStateManager.currentPhase = StateManager.TurnPhase.DONE_ANIMATION;
                        break;

                    case Direction.BACKWARD:
                        theStateManager.currentPhase = StateManager.TurnPhase.TURN_FINISH;
                        break;

                    case Direction.TO:
                        theStateManager.currentPhase = StateManager.TurnPhase.CHECK_FOR_CHALLENGE;
                        break;
                }                 isAnimating = false;             }         } else {             /* Another option of animation             this.transform.position = Vector3.SmoothDamp(                 this.transform.position,                 finalPosition,                 ref velocity,                 smoothTime);             */              // Constant speed animation             this.transform.position = Vector3.MoveTowards(                 this.transform.position,                 finalPosition,                 Time.deltaTime * speed);         } 	}      public int MoveForward(int steps) {         if (theStateManager.currentPhase == StateManager.TurnPhase.WAITING_FOR_ANIMATION) {             moveQueue = new Vector3[steps];             for (int i = 0; i < steps; i++) {                 if (finalTile < theStateManager.tiles.Length - 1) {                     moveQueue[i] = theStateManager.tiles[++finalTile].transform.position;                     this.steps++;                     moveQueueIndex = this.steps;                     isAnimating = true;                     dir = Direction.FORWARD;                 } else {                     Debug.Log("Win");                     theStateManager.gameWon = true;                     return finalTile;                 }             }             return finalTile;         }
        // error
        Debug.Log("Wrong call in MoveForward");         return -1;     }      public int MoveBackward(int steps) {
        if (theStateManager.currentPhase == StateManager.TurnPhase.WAITING_FOR_ANIMATION) {             moveQueue = new Vector3[steps];             for (int i = 0; i < steps; i++) {                 if (finalTile > 0) {                     moveQueue[i] = theStateManager.tiles[--finalTile].transform.position;                     this.steps++;                     moveQueueIndex = this.steps;                     isAnimating = true;                     dir = Direction.BACKWARD;                 } else {
                    return finalTile;
                }             }             return finalTile;         }
        // error
        Debug.Log("Wrong call in MoveBackward");         return -1;
    }      public int MoveTo (int tileIndex) {
        if (theStateManager.currentPhase == StateManager.TurnPhase.WAITING_FOR_ANIMATION) {
            moveQueue = new Vector3[1];
            finalTile = tileIndex;
            moveQueue[0] = theStateManager.tiles[finalTile].transform.position;
            this.steps = 1;
            moveQueueIndex = 1;
            isAnimating = true;
            dir = Direction.TO;
            return tileIndex;
        }

        return -1;
    } } 