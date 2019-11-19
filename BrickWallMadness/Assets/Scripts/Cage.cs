using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour {

    public Transform cage;

    public bool moving = false;
    public bool open = false;
    public float moveSpeed = 1f;
    public float perc = 0f;

    public Vector3 closedPos;
    public Vector3 openPos;

    public bool allTriggersActivated = false;
    public Trigger[] triggers;

    void Start()
    {
        if (!open)
        {
            closedPos = cage.transform.position;
            openPos = closedPos + (Vector3.up * 2.01f);
        }
        else if (open)
        {
            openPos = cage.transform.position;
            closedPos = openPos + (Vector3.down * 2.01f);
        }
    }

	// Update is called once per frame
	void Update () {

        if (!allTriggersActivated)
        {
            CheckTriggers();
        }

		if (moving)
        {
            if (!open)
            {
                Move(closedPos, openPos);
            }
            else if (open)
            {
                Move(openPos, closedPos);
            }
        }
	}

    public void Move(Vector3 startPos, Vector3 targetPos)
    {
        perc += Time.deltaTime;
        cage.transform.position = Vector3.Lerp(startPos, targetPos, moveSpeed * perc);
        if (moveSpeed * perc >= 1)
        {
            moving = false;
            perc = 0f;
            open = !open;
        }
    }

    void CheckTriggers()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            if (triggers[i].triggered == false)
            {
                allTriggersActivated = false;
                return;
            }
        }
        allTriggersActivated = true;
        moving = true;
    }

}
