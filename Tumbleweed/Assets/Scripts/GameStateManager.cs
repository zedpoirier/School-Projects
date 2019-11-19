// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Game State Manager contains all the persistent data throughout the game and 
/// manages the scene changes.
/// </summary>
public class GameStateManager : MonoBehaviour
{
    [Tooltip("Instance of the first GSM")]                          static GameStateManager instance;
    [Tooltip("Master score variable, carried through scenes")]      public int highScore;
    [Tooltip("Temp score for scene transitions")]                   public int currentScore;
    [Tooltip("Bool to keep score carried over from last game")]     public bool keepScore;
    [Tooltip("Reference to the UIController")]                      private UIController uIController;

    public static GameStateManager Accessor {
        get { return instance; }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (uIController == null) {
            uIController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController>();
        }
        if (highScore < uIController.scoreAmount) {
            highScore = uIController.scoreAmount;
        }
    }
}
