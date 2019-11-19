using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniReveal : MonoBehaviour {

    public Renderer oni;
    public AudioSource scream;
    public Collider wall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            oni.enabled = true;
            wall.enabled = true;
            Collider col = GetComponent<Collider>();
            col.isTrigger = false;
            scream.Play();
        }
    }
}
