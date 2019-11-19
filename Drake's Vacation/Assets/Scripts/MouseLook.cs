using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public bool isDrunk = false;
    public float drunkTime = 5f;
    [Range(0f,0.9f)]
    public float centralBuffer = 0.5f;
    public float rotationSpeed = 1.5f;

    Vector3 mousePosition;
    Vector2 rawRotation;
    float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        // Get mouse screen pos from -1 to 1 in x and y axis;
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition) * 2;
        mousePosition.x--;
        mousePosition.y--;

        // Check if X pos is greater or less than buffer;
        if (mousePosition.x > centralBuffer)
        {
            rawRotation.x = mousePosition.x - centralBuffer;
        }
        else if (mousePosition.x < -centralBuffer)
        {
            rawRotation.x = mousePosition.x + centralBuffer;
        }
        else
        {
            rawRotation.x = 0f;
        }

        // Check if Y pos is greater or less than buffer;
        if (mousePosition.y > centralBuffer)
        {
            rawRotation.y = mousePosition.y - centralBuffer;
        }
        else if (mousePosition.y < -centralBuffer)
        {
            rawRotation.y = mousePosition.y + centralBuffer;
        }
        else
        {
            rawRotation.y = 0f;
        }

        if (timer > 0)
        {
            isDrunk = true;
        }
        else if (timer <= 0)
        {
            isDrunk = false;
        }
        // Rotate player in the X and Y directions by rotationSpeed;
        // Drunk Mode
        Vector3 finalRot = rawRotation * rotationSpeed;
       
        if (isDrunk)
        {
            Quaternion quat = Quaternion.Euler(-finalRot.y, finalRot.x, 0);
            transform.rotation *= quat;
            
            
        }
        // Sober Mode // Broken
        else
        {
            Quaternion quat = Quaternion.Euler(0, finalRot.x, 0);
            transform.rotation *= quat;

        }


    }

    public void DrinkTheRum()
    {
        timer = drunkTime;
    }
}