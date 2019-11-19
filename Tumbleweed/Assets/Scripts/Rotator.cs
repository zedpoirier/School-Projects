// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rotator simply rotates the object along the desired axis by the desired speed per second
/// </summary>
public class Rotator : MonoBehaviour {

    [Tooltip("Rotation speed per second")]  public Vector3 rotationSpeed;

	void Update ()  {
        Rotate();
    }

    // Rotate
    /// <summary> Creates three floats for rotaation amounts in each axis. Using the rotation
    /// speed values multiplied by deltaTime. </summary>
    void Rotate() {
        float xRot = rotationSpeed.x * Time.deltaTime;
        float yRot = rotationSpeed.y * Time.deltaTime;
        float zRot = rotationSpeed.z * Time.deltaTime;
        transform.rotation = transform.rotation * Quaternion.Euler(xRot, yRot, zRot);
    }
}
