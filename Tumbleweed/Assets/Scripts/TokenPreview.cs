// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Token Preview moves the preview token object within view if the casted ray is colliding
/// with the spawnzone and if there is no currently active token in play.
/// </summary>
public class TokenPreview : MonoBehaviour {

    [Header("Preview Parameters")]
    [Tooltip("Flag to check if the cursor is within the spawnzone")]    public bool withinSpawnZone;
    [Tooltip("Reference to the spawner object")]                        public Spawner spawner;
    [Tooltip("Spawn location for tokens")]                              public Vector3 spawnPoint;
    [Tooltip("The position for the token preview when not in use")]     private Vector3 parkedPosition = new Vector3(0, -4, 0);

	void Update () {
        Preview();
    }

    // Preview
    /// <summary> Cast a ray outwards from the camera to the mouse position. If the ray hits 
    /// the spawnzone collider, place the preview of the token into the scene at the point 
    /// of contact, also hide the preview token below the ground when not needed</summary>
    void Preview() {
        if (!spawner.paused && !spawner.activeToken.gameObject.activeSelf && spawner.tokensRemaining != 0) {
            Ray ray;
            if (Input.touchCount != 0) {
                ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            }
            else {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);
            if (hitInfo.collider == spawner.GetComponent<Collider>()) {
                transform.position = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                spawnPoint = transform.position;
                withinSpawnZone = true;
            }
            else {
                transform.position = parkedPosition;
                withinSpawnZone = false;
            }
        }
        else {
            transform.position = parkedPosition;
            withinSpawnZone = false;
        }
    }
}