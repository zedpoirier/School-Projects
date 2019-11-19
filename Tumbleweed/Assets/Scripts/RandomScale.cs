// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Random Scale resizes the object once during it's creation to a random size 
/// within the max and min height and width values. </summary>
public class RandomScale : MonoBehaviour {

    [Header("Scale Parameters")]
    [Tooltip("Translate object up?")]   public bool raise;
    [Tooltip("Maximum height value")]   public float maxHeight = 3f;
    [Tooltip("Minimum height value")]   public float minHeight = 0.5f;
    [Tooltip("Maximum width value")]    public float maxWidth = 3f;
    [Tooltip("Minimum width value")]    public float minWidth = 0.5f;

    void Start() {
        Scale();
    }

    // Scale
    /// <summary> Randomly generate a height and width value from our bounds and
    /// apply it to the object's transform scale. </summary>
    void Scale() {
        float height = Random.Range(minHeight, maxHeight);
        float width = Random.Range(minWidth, maxWidth);
        transform.localScale = new Vector3(width, height, width);
        if (raise) {
            transform.position = new Vector3(transform.position.x, height / 2, transform.position.z);
        }
    }
}
