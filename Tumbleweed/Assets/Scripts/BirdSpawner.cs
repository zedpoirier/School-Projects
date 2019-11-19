// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Bird Controller will spawn birds that will fly across the screen in the specified
/// direction. It spawns the birds with an interval.
/// </summary>
public class BirdSpawner : MonoBehaviour {

    [Header("Spawn Parameters")]
    [Tooltip("During of the life of the birds")]                public float lifetime = 60f;
    [Tooltip("Min and max values for spawn rate")]              public Vector2 spawnRateRange = new Vector2(1f, 5f);
    [Tooltip("Direction of movement for the birds")]            public Vector3 initialVelocity;
    [Tooltip("Timer to count for spawn intervals")]             private float timer;
    [Tooltip("The time between spawns in seconds")]             private float spawnRate = 0f;
    [Tooltip("Reference to the Pool of Birds")]                 private ObjectPool birdPool;
	
    void Start() {
        birdPool = GameObject.FindGameObjectWithTag("BirdPool").GetComponent<ObjectPool>();
        spawnRate = Random.Range(spawnRateRange.x, spawnRateRange.y);
    }

	void Update() {
        SpawnBirds();
	}

    // Spawn Birds
    /// <summary> Spawns a bird form the spawn point once the timer reaches the spawnRate.
    /// The spawnRate is generated from a range of values. The bird is givin an initial
    /// velocity and set to destroy after one minute. </summary>
    void SpawnBirds() {
        timer += Time.deltaTime;
        if (timer >= spawnRate) {
            timer = 0;
            spawnRate = Random.Range(spawnRateRange.x, spawnRateRange.y);
            GameObject birdy;
            birdy = birdPool.Spawn(transform.position);
            birdy.transform.rotation = transform.rotation;
            birdy.GetComponent<Rigidbody>().velocity = initialVelocity;
        }
    }
}
