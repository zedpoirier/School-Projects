// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Goal controls the win areas for the token. If the token stays within the collider
/// for a at least the max time the token will be destroyed and the points for that
/// goal will be added to the score amount.
/// </summary>
public class Goal : MonoBehaviour {

    [Header("Goal Parameters")]
    [Tooltip("Flag to check if the token is within the collider")]              public bool withinGoal = false;
    [Tooltip("Flag to check if the token is out of bounds")]                    public bool outOfBounds = false;
    [Tooltip("The number of points awarded if the token rests in this goal")]   public int points = 50;
    [Tooltip("Timer counts how long the token has been within the goal")]       public float maxTime = 3f;
    [Tooltip("Timer counts how long the token has been within the goal")]       public float goalTimer = 0f;
    [Header("References")]
    [Tooltip("Reference to the Spawner")]                                       public Spawner spawner;
    [Tooltip("Reference to the UI Controller")]                                 public UIController canvas;
    [Tooltip("Reference to the Audio Manager")]                                 private AudioManager audioManager;
    [Tooltip("Reference to the LEvel Manager")]                                 private LevelManager levelManager;

    void Start() {
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
        levelManager = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
    }

    void Update () {
        GoalCheck();
    }

    void OnTriggerEnter(Collider token) {
        if(token.tag == "Token") {
            withinGoal = true;
        }
    }

    void OnTriggerExit(Collider token) {
        if(token.tag == "Token") {
            withinGoal = false;
            goalTimer = 0;
        }
    }

    // Goal Check
    /// <summary> Check if the token is within the goal collider. If so, then start the
    /// timer. When timer hits the max time threshold, reset the timer, assign points to
    /// the score, play the gunshot clip, and destroy the token. If the goal is marked
    /// as out of bounds then give back one token to the player and show text. </summary>
    void GoalCheck() {
        if (withinGoal) {
            goalTimer += Time.deltaTime;
            if (goalTimer >= maxTime) {
                withinGoal = false;
                goalTimer = 0;
                levelManager.currentScore += points;
                canvas.changeAmount = points;
                canvas.targetAmount = levelManager.currentScore;
                audioManager.Shot();
                spawner.activeToken.SetActive(false);
                if (outOfBounds) {
                    spawner.tokensRemaining++;
                    canvas.freeToken.gameObject.SetActive(true);
                    canvas.freeTokenShadow.gameObject.SetActive(true);
                }
            }
        }
    }
}
