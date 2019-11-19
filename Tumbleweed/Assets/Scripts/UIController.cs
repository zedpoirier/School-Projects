// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary> 
/// UI Controller will manage the token UI spacing and adjust it as needed. It will 
/// also keep the score UI updated with the current score.
/// </summary>
public class UIController : MonoBehaviour {

    [Header("Token Parameters")]
    [Tooltip("Basic padding for the UI tokens, applied to left, right, and top")]   public float tokenPadding = 20f;
    [Tooltip("Reference to tokens remaining from the Spawner")]                     private int tokenCount = 0;
    [Tooltip("Calculated spacing to evenly spread out the UI tokens")]              private float tokenSpacing;
    [Header("Score Parameters")]
    [Tooltip("Reference to the score object")]                                      public Text score;
    [Tooltip("Reference to the score shadow")]                                      public Text scoreShadow;
    [Tooltip("Reference to the high score object")]                                 public Text highScore;
    [Tooltip("Reference to the high score shadow")]                                 public Text highScoreShadow;
    [Tooltip("Reference to the high score object")]                                 public Text victoryScore;
    [Tooltip("Reference to the high score shadow")]                                 public Text victoryScoreShadow;
    [Tooltip("Reference to the free token object")]                                 public Text freeToken;
    [Tooltip("Reference to the free token shadow")]                                 public Text freeTokenShadow;
    [Tooltip("Total amount of points in the current game")]                         public int scoreAmount;
    [Tooltip("Amount used to set the score value over time")]                       public int targetAmount;
    [Tooltip("Amount used to determine the speed of change in the score")]          public int changeAmount;
    [Tooltip("String for beginning of the score text")]                             private string preScoreText = "Score: ";
    [Tooltip("String for beginning of the high score text")]                        private string preHighScoreText = "High Score: ";
    [Tooltip("String for beginning of the high score text")]                        public string preVictoryScoreText = "Score To Win: ";
    [Header("References")]
    [Tooltip("Reference to the Spawner")]                                           public Spawner spawner;
    [Tooltip("Reference to the UI Play Button")]                                    public GameObject playButton;
    [Tooltip("Reference to the UI NextAgain Button")]                               public GameObject nextButton;
    [Tooltip("Reference to the UI PrevAgain Button")]                               public GameObject prevButton;
    [Tooltip("Reference to the UI Continue Button")]                                public GameObject continueButton;
    [Tooltip("Reference to the UI Try Again Button")]                               public GameObject tryAgainButton;
    [Tooltip("Reference to the UI Quit Button")]                                    public GameObject quitButton;
    [Tooltip("Reference to the UI tokens, for looping purposes")]                   public RectTransform[] tokenImages;

    void Start() {
        highScore.text = preHighScoreText + GameStateManager.Accessor.highScore;
        highScoreShadow.text = highScore.text;
        if (GameStateManager.Accessor.keepScore) {
            scoreAmount = GameStateManager.Accessor.currentScore;
            score.text = preScoreText + scoreAmount;
            scoreShadow.text = score.text;
        }
    }

    void Update () {
        TokenSpacing();
        ScoreUpdate();
    }

    // Token Spacing
    /// <summary> Check if the token count is not equal to the tokens remaining 
    /// from the spawner object. If not, set the new token count and calculate
    /// the spacing value for each token using the screen width. Loop through 
    /// each of the UI tokens, and place them equal widths apart. </summary>
    void TokenSpacing() {
        if (tokenCount != spawner.tokensRemaining) {
            tokenCount = spawner.tokensRemaining;
            if (tokenCount == 0) {
                for (int i = 0; i < tokenImages.Length; i++) {
                    tokenImages[i].anchoredPosition = new Vector2(tokenPadding, 1000);
                }
            }
            else {
                tokenSpacing = 1600 / tokenCount;
                for (int i = 0; i < tokenImages.Length; i++) {
                    if (i < tokenCount) {
                        float xPos = tokenSpacing / 2 + (i * tokenSpacing);
                        tokenImages[i].anchoredPosition = new Vector2(xPos, -tokenPadding);
                    }
                    else {
                        tokenImages[i].anchoredPosition = new Vector2(tokenPadding, 1000);
                    }
                }
            }
        }
    }

    // Score Update
    /// <summary> Takes the preScoreText and the current score amount and changes 
    /// the text of the UI Score object to show the current score. It also updates
    /// the high score if a new record is set. </summary>
    void ScoreUpdate() {
        if (scoreAmount < targetAmount) {
            scoreAmount += changeAmount / 50;
            if (scoreAmount > targetAmount) {
                scoreAmount = targetAmount;
            }
        }
        score.text = preScoreText + scoreAmount;
        scoreShadow.text = score.text;
        if (scoreAmount > GameStateManager.Accessor.highScore) {
            GameStateManager.Accessor.highScore = scoreAmount;
            highScore.text = preHighScoreText + scoreAmount;
            highScoreShadow.text = highScore.text;
        }
    }

    // Play Game
    /// <summary> Hides the play button and starts the game. </summary>
    public void PlayGame() {
        playButton.SetActive(false);
        nextButton.SetActive(false);
        prevButton.SetActive(false);
        spawner.paused = false;
        GameStateManager.Accessor.keepScore = false;
    }

    // Change Level
    /// <summary> Changes the current scene to the selected scene. </summary>
    /// <param name="index"></param>
    public void ChangeLevel(int index) {
        SceneManager.LoadScene(index);
    }

    // Quit Game
    /// <summary> Quit the game. </summary>
    public void QuitGame() {
        Application.Quit();
    }
}
