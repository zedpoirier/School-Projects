using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTarget : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("This object has no target!", this);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(target.position - transform.position);
        }
    }
}
