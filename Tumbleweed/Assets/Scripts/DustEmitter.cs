// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Dust Emitter will track the active token and measure it's properties. It will access the velocity,
/// and the height ot determine if the token should be creating dust as it rolls. If so then it will
/// spawn the dust prefab at the proper position relative to the current token.
/// </summary>
public class DustEmitter : MonoBehaviour {

    [Header("Dust Emitter Parameters")]
    [Tooltip("Value to check if token is airbourne")]       public float heightCheck = 1.1f;
    [Tooltip("Minimum speed for dust to spawn")]            public float minimumDustSpeed = 3;
    [Tooltip("Delay between particle spawns")]              public float dustDelay = 0.25f;
    [Tooltip("Offset position for the dust particles")]     public Vector3 dustPosition;
    [Tooltip("Flag checking for contact with the ground")]  private bool grounded = false;
    [Tooltip("Flag checking airborn status")]               private bool airborn = false;
    [Tooltip("Flag if the token is moving fast enough")]    private bool moving = false;
    [Tooltip("Reference to the token's speed")]             private float tokenSpeed;
    [Tooltip("Counter for the delay timer")]                private float timer = 0;
    [Header("References")]
    [Tooltip("Reference to the audio manager")]             public AudioManager audioMananger;
    [Tooltip("Reference to the Dust Pool")]                 private ObjectPool dustPool;
	
    void Start() {
        audioMananger = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
        dustPool = GameObject.FindGameObjectWithTag("DustPool").GetComponent<ObjectPool>();
    }

	void Update () {
        GroundCheck();
        SpeedCheck();
        DustSpawn();
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Ground") {
            audioMananger.Thud();
        }
        else if (other.gameObject.tag == "Ground" && airborn) {
            audioMananger.Thud();
            airborn = false;
        }
    }

    // Ground Check
    /// <summary> Checks if the token is below the heighCheck threshold, if
    /// so then set the grounded flag to true. </summary>
    void GroundCheck() {
        if (transform.position.y <= heightCheck) {
            grounded = true;
        }
        else if(transform.position.y > heightCheck) {
            grounded = false;
            airborn = true;
        }
    }

    // Speed Check
    /// <summary> Checks the magnitude of velocity of the token, if the token is 
    /// moving faster than the minimum threshold set movign to true.</summary>
    void SpeedCheck() {
        tokenSpeed = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        if (tokenSpeed >= minimumDustSpeed) {
            moving = true;
        }
        else if (tokenSpeed < minimumDustSpeed) {
            moving = false;
        }
    }

    // Dust Spawn
    /// <summary> Spawns the dust particle prefab if all the conditions are met.
    /// token is grounded, token is moving, and the timer is greater than the 
    /// delay value divided by the tokenSpeed. </summary>
    void DustSpawn() {
        timer += Time.deltaTime;
        if (timer >= (dustDelay / tokenSpeed) && grounded && moving) {
            timer = 0;
            //GameObject dusty;
            dustPool.Spawn(transform.position + dustPosition);
        }
    }
}
