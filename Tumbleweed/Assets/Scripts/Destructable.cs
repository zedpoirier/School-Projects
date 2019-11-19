// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Destructable will destroy this object if the token collides with it.
/// </summary>
public class Destructable : MonoBehaviour {

    [Header("Destructable Parameters")]
    [Tooltip("Number of hits before destruction")]  public int hitPoints = 1;
    [Tooltip("Spawn location for the puffs")]       private Vector3 spawnPoint;
    [Tooltip("Reference to the Pool of Puffs")]     private ObjectPool puffPool;
	
    void Start() {
        puffPool = GameObject.FindGameObjectWithTag("PuffPool").GetComponent<ObjectPool>();
        spawnPoint = transform.position;
    }
    
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Token") {
            hitPoints--;
            if (hitPoints <= 0) {
                GameObject puffy;
                puffy = puffPool.Spawn(spawnPoint);
                puffy.transform.rotation = transform.rotation;
                gameObject.SetActive(false);
            }
        }
    }
}
