// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>  
/// Spawner creates a token from the prefab object when there is no token in play and the player
/// clicks the left mouse button. It also tracks the number of tokens remaining before game over.
/// </summary>
public class Spawner : MonoBehaviour {

    [Header("Spawning Parameters")]
    [Tooltip("Is the game paused/in menu?")]                                public bool paused = true;
    [Tooltip("Starting number of tokens")][Range(1, 5)]                     public int tokensStartAmount = 5;
    [Tooltip("Number of remaining tokens beofre game over")]                public int tokensRemaining = 0;
    [Tooltip("The current token in the scene")]                             public GameObject activeToken;
    [Tooltip("Reference to the token preview object in scene")]             public TokenPreview preview;
    [Header("References")]
    [Tooltip("Reference to the UI Controller on the canvas")]               public UIController canvas;
    [Tooltip("Reference to the Level Manager")]                             public LevelManager levelManager;
    [Tooltip("Reference to the Token Pool")]                                private ObjectPool tokenPool;

    private void Start() {
        tokensRemaining = tokensStartAmount;
        tokenPool = GameObject.FindGameObjectWithTag("TokenPool").GetComponent<ObjectPool>();
        activeToken = tokenPool.Spawn(preview.spawnPoint);
        activeToken.SetActive(false);
    }

    void Update() {
        SpawnToken();
        GameOver();
    }

    // Spawn Token
    /// <summary> If there's no currently active token and the preview is acive inside the spawnzone then
    /// left clicking the mouse will spawn a token from the prefab at the the preview's position. Also
    /// reduce the number of tokens left and set this token as the active camera target. </summary>
    void SpawnToken() {
        if (!activeToken.activeSelf && tokensRemaining > 0 && preview.withinSpawnZone && !paused) {
            if (Input.touchCount != 0) {
                if (Input.touches[0].phase == TouchPhase.Ended) {
                    activeToken = tokenPool.Spawn(preview.spawnPoint);
                    activeToken.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Camera.main.GetComponent<CameraController>().ChangeTarget(activeToken.transform);
                    tokensRemaining--;
                }
            }
            else if (Input.GetMouseButtonDown(0)) {
                activeToken = tokenPool.Spawn(preview.spawnPoint);
                activeToken.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Camera.main.GetComponent<CameraController>().ChangeTarget(activeToken.transform);
                tokensRemaining--;
            }
        }
    }

    // Game Over
    /// <summary> Checks if the number of remaining tokens is zero and if there is no current token
    /// inside the scene. If so, then activate the Play Again UI button. This also checks if the
    /// victory condition for the level has been met, if so save the current score in GSM. </summary>
    void GameOver() {
        if (tokensRemaining == 0 && !activeToken.activeSelf) {
            if (canvas.targetAmount >= levelManager.victoryScore) {
                GameStateManager.Accessor.keepScore = true;
                GameStateManager.Accessor.currentScore = canvas.scoreAmount;
                canvas.continueButton.SetActive(true);
                canvas.quitButton.SetActive(true);
            }
            else {
                canvas.tryAgainButton.SetActive(true);
                canvas.quitButton.SetActive(true);
                GameStateManager.Accessor.currentScore = 0;
            } 
        }
    }
}