using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDeath : MonoBehaviour
{
    public UnityEvent onDeath;

    public void Die()
    {
        onDeath.Invoke();
    }
}
