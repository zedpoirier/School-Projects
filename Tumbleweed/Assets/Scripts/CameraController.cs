// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>  
/// Camera Controller checks for active tokens, moves the camera to follow the token, rotates the 
/// camera to follow the token and is able to change the camera target if needed.
/// </summary>
public class CameraController : MonoBehaviour {

    [Header("Movement Parameters")]
    [Tooltip("Subtracted from the z axis")]                         public float verticalOffset = 5f;
    [Tooltip("The percentage of Lerp smoothing")][Range(0f, 1f)]    public float lerpPercentage;
    [Tooltip("The maximum boundaries using X for X and Y for Z")]   public Vector2 maxBounds = new Vector2(2f, 0f);
    [Tooltip("The minimum boundaries using X for X and Y for Z")]   public Vector2 minBounds = new Vector2(-2f, -100f);
    [Tooltip("Transform of the spawn zone in the level")]           public Transform spawnZone;
    [Tooltip("Camera will follow this object")]                     public Transform currentTarget;

	void Update () {
        CheckTarget();
        PositionUpdate();
        RotationUpdate();
    }

    // Check Target
    /// <summary> Checks if the current camera target is null. If so then check to find an active Token 
    /// object. If not Token exists in the scene, set currentTarget to the spawn position. </summary>
    public void CheckTarget() {
        if (!currentTarget.gameObject.activeSelf) {
            if (GameObject.FindGameObjectWithTag("Token") == null) {
                ChangeTarget(spawnZone);
            }
            else {
                ChangeTarget(GameObject.FindGameObjectWithTag("Token").transform);
            }
        }
    }

    // Position Update
    /// <summary> Move the camera relative to it's target. The X position is clamped to limit the movement. 
    /// The Z position is also clamped to the bounds of the game board. Each call the position will be 
    /// lerped towards the target position by the lerp percentage value multiplied by deltaTime. </summary>
    void PositionUpdate() {
        float xPos = Mathf.Clamp(currentTarget.transform.position.x, minBounds.x, maxBounds.x);
        float zPos = Mathf.Clamp(currentTarget.transform.position.z - verticalOffset, minBounds.y, maxBounds.y);
        Vector3 targetPosition = new Vector3(xPos, transform.position.y, zPos);
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpPercentage * Time.deltaTime);
    }

    // Rotation Update
    /// <summary> Rotate the camera relative to it's target. A Vector3 pointing towards the target and a Vector3 
    /// pointing in the desired view angle are combined inside the LookRotation Quaternion method to get a target 
    /// rotation. Each call the rotation will be lerped towards the target rotation by the lerp percentage value.
    /// multiplied by deltaTime. </summary>
    void RotationUpdate() {
        Vector3 toTarget = currentTarget.position - transform.position;
        Vector3 cameraUp = new Vector3(0, 0.5f, 0.5f);
        Quaternion targetRotation = Quaternion.LookRotation(toTarget, cameraUp);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpPercentage * Time.deltaTime);
    }

    // Change Target
    /// <summary> Takes in a Transform and assigns it as the current camera target. </summary>
    public void ChangeTarget(Transform newTarget) {
        currentTarget = newTarget;
    }
}
