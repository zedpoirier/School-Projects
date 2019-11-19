// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Will grow and shrink the GO within the bounds assigned.
/// </summary>
public class SineGrow : MonoBehaviour {

    [Tooltip("Amplitude of the growth wave")]               public float growthFactor = 2f;
	
	void Update () {
        float timer = Mathf.Sin(Time.time * growthFactor);
        if (timer > 0) {
            GetComponent<Text>().fontSize++;
        }
        else {
            GetComponent<Text>().fontSize--;
        }
    }
}
