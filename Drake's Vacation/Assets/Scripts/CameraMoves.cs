using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoves : MonoBehaviour
{
    public bool playerDead = false;
    public float camSpeed = 0.5f;
    

    // Update is called once per frame
    void Update()
    {
        if (playerDead)
        {
            Vector3 targetPos = transform.position + Vector3.forward * 10;
            transform.position = Vector3.Lerp(transform.position, targetPos, camSpeed);
        }
    }

    public void PlayerDeath()
    {
        playerDead = true;
    }
}
