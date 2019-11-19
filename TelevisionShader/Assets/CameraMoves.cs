using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoves : MonoBehaviour {

    Transform start;
    public Transform target;
    bool panning = false;
    float timer = 0;
    public float duration = 2;

	// Use this for initialization
	void Start () {
        start = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            panning = true;
        }
        if (panning)
        {
            timer += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start.position, target.position, timer);
            transform.rotation = Quaternion.Lerp(start.rotation, target.rotation, timer);
            if (timer >= 1)
            {
                panning = false;
            }
        }
	}
}
