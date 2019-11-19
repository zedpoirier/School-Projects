// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Stop Destroyer checks if this object has stopped moving. If it has stopped for more than
/// the maximum time period it will be destroyed.
/// </summary>
public class StopDestroyer : MonoBehaviour {

    [Header("Destroyer Parameters")]
    [Tooltip("Time limit before the destroy happens")]              public float maxTime = 3;
    [Tooltip("Tolerance for small movements")]                      public float tolerance = 0.01f;
    [Tooltip("Timer that will count up when token is stopped")]     private float timer = 0;
    [Tooltip("Position of the token on the last frame")]            private Vector3 lastPosition;
    [Tooltip("Reference to the Audio Manager")]                     private AudioManager audioManager;

    void Update () {
        Destroyer();
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }

    // Destroyer
    /// <summary> Checks if the curretn position is equal to the previous position, if so then 
    /// start the timer. If the timer reaches the time limit, destroy object. </summary>
    void Destroyer() {
        float distance = Vector3.Distance(transform.position, lastPosition);
        if (distance <= tolerance) {
            timer += Time.deltaTime;
            if (timer >= maxTime) {
                gameObject.SetActive(false);
                audioManager.Shot();
            }
        }
        else {
            timer = 0;
        }
        lastPosition = transform.position;
    }

    // Destroy Faster
    /// <summary> Method used to destroy an object instantly if needed. </summary>
    public void DestroyFaster() {
        gameObject.SetActive(false);
    }
}
