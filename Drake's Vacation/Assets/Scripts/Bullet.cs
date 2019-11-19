using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        //Vector3 motion = -transform.forward * speed * Time.deltaTime;
        //transform.Translate(0, 0, motion.z, Space.Self);
       
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Sorry!");
            return;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("I hit an Enemy");
            OnDeath ded = other.GetComponent<OnDeath>();
            if (ded != null) { ded.Die(); }
        }
        else if (other.gameObject.tag == "Hostage")
        {
            Debug.Log("I hit the hostage D:");
            other.GetComponent<OnDeath>().Die();
            // Also reset gamestate
        }
        else if (other.gameObject.tag == "Explode")
        {
            Debug.Log("BOOOOM");
        }
        else
        {
            Debug.Log("I hit an object.");
        }
        Destroy(gameObject);
    }
}
