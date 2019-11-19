using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SimplerTeleport : MonoBehaviour {

    public Transform player;
    public Transform destination;
    public float angleTwist;
    public Vector3 posistionOffset;
    public bool textChanger = false;
    private bool playerIsOverlapping;
    private TextController textController;


    // Use this for initialization
    void Start () {
        posistionOffset = destination.position - transform.position;
        textController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<TextController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerIsOverlapping)
        {
            player.GetComponent<FirstPersonController>().isTeleporting = true;
            player.position += posistionOffset;
            player.RotateAround(destination.position, Vector3.up, angleTwist);
            playerIsOverlapping = false;
            if (textChanger)
            {
                textController.NextText();
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }
}
