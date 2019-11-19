// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Tracks the bird's current lifetime and sets the GO to false
/// </summary>
public class Lifetime : MonoBehaviour {

    [Tooltip("Total lifetime for the object")]      public float lifetime;
    [Tooltip("Timer to count to lifetime")]         private float timer;

    void Start() {
        timer = lifetime;
    }

    void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            gameObject.SetActive(false);
            timer = lifetime;
        }
	}

    // Destroy Faster
    /// <summary> Method used to destroy an object instantly if needed. </summary>
    public void DestroyFaster() {
        gameObject.SetActive(false);
    }
}
