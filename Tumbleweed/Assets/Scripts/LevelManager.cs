// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Level Manager will contain the level victory requirements and reset the level without reloading the scene.
/// </summary>
public class LevelManager : MonoBehaviour {

    [Tooltip("Score threshold to continue playing the game")]       public int victoryScore = 1000;
    [Tooltip("Score threshold to continue playing the game")]       public int currentScore = 0;
    [Tooltip("List of all destructable elements in the level")]     private Destructable[] destructableElements;
    [Tooltip("Reference to the spawner object")]                    private Spawner spawner;
    [Tooltip("Reference to the UIController")]                      private UIController uIController;

	void Start () {
        destructableElements = FindObjectsOfType<Destructable>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        uIController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController>();
        currentScore = GameStateManager.Accessor.currentScore;
        victoryScore =  currentScore + 1000;
        uIController.scoreAmount = currentScore;
        uIController.victoryScore.text = uIController.preVictoryScoreText + victoryScore;
        uIController.victoryScoreShadow.text = uIController.preVictoryScoreText + victoryScore;
    }

    // Reset Level
    /// <summary> Restart the scene. </summary>
    public void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
