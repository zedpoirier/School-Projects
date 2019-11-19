using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [Tooltip("Parent object to the door model. Treat as a hinge.")]
    public Transform doorPivot;
    [Tooltip("Door model, make sure it is child to the pivot.")]
    public Transform door;
    public Door linkedDoor;
    private Quaternion closedPos;
    private Quaternion openPos;

    [Header("Player Values")]
    public float maxDistance = 3f;
    private float distance;
    private Transform player;

    [Header("Opening Values")]
    [Tooltip("Door wide open angle.")]
    public float openAngle = 100f;
    [Tooltip("Amount of seconds the door will take to open")]
    public float moveSpeed = 1f;
    private bool moving = false;
    private bool open = false;
    private float perc = 0f;

    [Header("Autoclose Settings")]
    [Tooltip("Do want the door to close automatically?")]
    public bool autoClose = true;
    [Tooltip("Delay in seconds, after the door finishes moving")]
    public float autoCloseDelay = 8f;
    private float closeTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        closedPos = transform.rotation;
        openPos = closedPos * Quaternion.Euler(0, openAngle, 0);
    }

    void Update () {
        distance = Vector3.Distance(player.position, door.transform.position);

        if (autoClose && open && !moving)
        {
            closeTimer += Time.deltaTime;
            if (closeTimer >= autoCloseDelay)
            {
                Move(openPos, closedPos);
            }
        }

        if (moving)
        {
            door.GetComponent<BoxCollider>().enabled = false;
            if (!open)
            {
                Move(closedPos, openPos);
            }
            else if (open)
            {
                Move(openPos, closedPos);
            }
        }
        else
        {
            door.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void Move(Quaternion startPos, Quaternion targetPos)
    {
        perc += (1 / moveSpeed) * Time.deltaTime;
        doorPivot.transform.rotation = Quaternion.Lerp(startPos, targetPos, perc);
        if (perc >= 1)
        {
            moving = false;
            perc = 0f;
            open = !open;
            closeTimer = 0f;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            if (distance <= maxDistance && !moving)
            {
                moving = true;
                if (linkedDoor != null)
                {
                    linkedDoor.moving = true;
                }
            }
        }
    }
}
