using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booze : MonoBehaviour
{
    public float rotationSpeed = 15f;
    MouseLook player;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //Float and spin
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        transform.position += Vector3.up * Mathf.Cos(time) * 0.0025f;
    }

    private void OnTriggerEnter(Collider other)
    {
        player.DrinkTheRum();
        Debug.Log("Let's drink!");
        Destroy(gameObject);
    }
}
