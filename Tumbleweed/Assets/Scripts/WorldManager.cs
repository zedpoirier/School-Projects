// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enables the switching of enviroments with key presses.
/// </summary>
public class WorldManager : MonoBehaviour {

    public GameObject[] worlds;

	void Start () {
        SwitchWorld(1);
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SwitchWorld(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SwitchWorld(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SwitchWorld(3);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().activeToken.SetActive(false);
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().tokensRemaining++;
        }
    }

    // World Switch
    /// <summary>Sets the world at the world index value active, and deactivates
    /// all other worlds in the scene.</summary>
    /// <param name="worldIndex">Value assinged to each world, starting at 0.</param>
    void SwitchWorld(int worldIndex) {
        for (int i = 0; i < worlds.Length; i++) {
            if (i != worldIndex - 1) {
                worlds[i].SetActive(false);
            }
            else if (i == worldIndex - 1) {
                worlds[i].SetActive(true);
            }
        }
    }
}
