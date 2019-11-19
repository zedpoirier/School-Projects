// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Random Rotation rotates the object a random amount in the z axis.
/// </summary>
public class RandomRotation : MonoBehaviour {

    [Tooltip("Lock rotations to 90 degrees")] public bool rightAngles;

	void Start () {
        Rotate();
    }

    // Rotate
    /// <summary> Randomly generate a float between 0 and 360 degrees. Apply that
    /// to the z axis rotation of the object. </summary>
    void Rotate() {
        float rand;
        if (rightAngles) {
            int[] angles = { 0, 90, 180, 270 };
            rand = angles[Random.Range(0, angles.Length)];
        }
        else {
            rand = Random.Range(0.0f, 360.0f);
        }
        transform.RotateAround(transform.position, Vector3.up, rand);
    }
}
