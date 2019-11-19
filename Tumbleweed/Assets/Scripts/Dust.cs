// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Tracks the dust's current lifetime and sets the GO to false
/// </summary>
public class Dust : MonoBehaviour
{

    [Header("Dust Parameters")]
    [Tooltip("Duration of the Dust effect")]        public float lifetime = 2f;
    [Tooltip("Timer to change the dust's state")]   public float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            gameObject.SetActive(false);
        }
    }
}
