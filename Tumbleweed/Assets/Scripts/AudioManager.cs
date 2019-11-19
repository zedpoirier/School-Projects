// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Audio Clip Player will manage all of the ambient soundeffects.
/// </summary>
public class AudioManager : MonoBehaviour {

    [Header("References to the audio clips")]
    [Tooltip("Refence to the bird clips")]                  public AudioClip[] birds;
    [Tooltip("Refence to the thud clips")]                  public AudioClip[] thuds;
    [Header("References to the SFX Players")]
    [Tooltip("Reference to the sfx player for birds")]      public AudioSource birdsPlayer;
    [Tooltip("Reference to the sfx player for thuds")]      public AudioSource thudPlayer;
    [Tooltip("Reference to the sfx player for winning")]    public AudioSource shotPlayer;
    [Header("Bird SFX Paramters")]
    [Tooltip("The min and max values of the intervals")]    public Vector2 birdRange;
    [Tooltip("The scurrent interval delay")]                private float birdInterval = 2;
    [Tooltip("Timer for determining bird intervals")]       private float birdTimer;
    
    void Start() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Sound");
        if (objs.Length > 1) {
            Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

	void Update () {
        Birds();
	}

    // Birds
    /// <summary> Will play a random clip from one of the bird audio clips at 
    /// random intervals between the birdRange. </summary>
    void Birds() {
        if (!birdsPlayer.isPlaying) {
            birdTimer += Time.deltaTime;
            if (birdTimer >= birdInterval) {
                birdTimer = 0;
                birdInterval = Random.Range(birdRange.x, birdRange.y);
                birdsPlayer.clip = birds[Random.Range(0, birds.Length)];
                birdsPlayer.Play();
            }
        }
    }

    // Thud
    /// <summary> Will play a random clip from one of the thud audio 
    /// clips when triggered. Triggered by the Dust Emitter script.
    /// For relevant height and velocity data. </summary>
    public void Thud() {
        thudPlayer.clip = thuds[Random.Range(0, thuds.Length)];
        thudPlayer.Play();
    }

    // Shot
    /// <summary> Will play the audio clip from shotPlayer when called.
    /// Called by the </summary>
    public void Shot() {
        shotPlayer.Play();
    }
}