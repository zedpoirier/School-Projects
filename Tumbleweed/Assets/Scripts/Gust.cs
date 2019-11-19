// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Bump
/// </summary>
public class Gust : MonoBehaviour {

    [Tooltip("Number of bumps left")]                           public int gusts = 3;
    [Tooltip("Duration of wind force applied")]                 public float blowTime = 3.0f;
    [Tooltip("Strength of force of the gust")][Range(1, 5)]     public float gustForce = 3.0f;
    [Tooltip("Strength of force of the bump horizontally")]     public float offsetGustSpawn = 20.0f;
    [Tooltip("Vector for gust force")]                          private float gustVector;
    [Tooltip("Timer to track time blowing")]                    private float gustTimer;
    [Tooltip("Reference to the activeToken in spawner")]        private Spawner spawner;
    [Tooltip("Reference to the Gust Pool")]                     private ObjectPool gustPool;
	
    void Start() {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        gustPool = GameObject.FindGameObjectWithTag("GustPool").GetComponent<ObjectPool>();
        gustTimer = blowTime;
    }

	void Update () {
        ControllerDesktop();
        ControllerMobile();
        Blow();
    }

    //Controller Desktop
    /// <summary> Check for left or right input in desktop controls. </summary>
    void ControllerDesktop() {
        if (spawner.activeToken != null && gusts > 0) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                GustSpawn(false);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                GustSpawn(true);
            }
        }
    }

    // Controller Mobile
    /// <summary> Check for touch on the screen. If touch is on the left half of the screen
    /// spawn Gust on the opposite side so the token moves towards the touch. </summary>
    void ControllerMobile() {
        if (Input.touches.Length > 0 && spawner.activeToken != null && gusts > 0) {
            if (Input.touches[0].position.x <= Screen.width / 2 && Input.touches[0].phase == TouchPhase.Began) {
                GustSpawn(true);
            }
            else if (Input.touches[0].position.x > Screen.width / 2 && Input.touches[0].phase == TouchPhase.Began) {
                GustSpawn(false);
            }
        }
    }

    // Gust Spawn
    /// <summary>  </summary>
    /// <param name="blowingLeft"></param>
    void GustSpawn(bool blowingLeft) {
        Vector3 gustLocation;
        if (blowingLeft) {
            gustLocation = new Vector3(spawner.activeToken.transform.position.x + offsetGustSpawn, 2, spawner.activeToken.transform.position.z);
            gustVector = -gustForce;
        }
        else {
            gustLocation = new Vector3(spawner.activeToken.transform.position.x + -offsetGustSpawn, 2, spawner.activeToken.transform.position.z);
            gustVector = gustForce;
        }
        GameObject gusty = gustPool.Spawn(gustLocation);
        gusty.transform.LookAt(spawner.activeToken.transform, Vector3.up);
        gusts--;
        gustTimer = 0;
    }

    void Blow() {
        gustTimer += Time.deltaTime;
        if (gustTimer <= blowTime)
        {
            spawner.activeToken.GetComponent<Rigidbody>().AddForce(gustVector, 0, 0);
        }
    }
}
