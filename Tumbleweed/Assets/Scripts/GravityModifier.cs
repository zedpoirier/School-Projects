// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Gravity Modifier will apply additional forces to an object to simulate gravity.
/// </summary>
public class GravityModifier : MonoBehaviour {

    [Tooltip("Vectors of gravity to apply to the object")] public Vector3 gravityMod;

	void Update () {
        GetComponent<Rigidbody>().AddForce(gravityMod);
	}
}
