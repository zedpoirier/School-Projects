using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTv : MonoBehaviour {

    Renderer rendy;
    bool activated = false;
    AudioSource hz;
    string horizontalMask = "Vector1_C9909430";
    string verticalMask = "Vector1_1C4FD9D5";
    string strength = "Vector1_EEEEB786";
    float horizontalValue = 0;
    float verticalValue = 0;
    float strengthValue = 50;
    float timer = 0;
    bool turningOn = false;
    bool turningOff = false;
    public float speed = 0.5f;
    public bool startOn = false;

	// Use this for initialization
	void Start () {
        rendy = GetComponent<Renderer>();
        hz = GetComponent<AudioSource>();
        if (startOn)
        {
            turningOn = true;
            rendy.materials[0].SetFloat("Vector1_36F8660A", 1.0f);
            rendy.materials[1].SetFloat("Vector1_A5282C4", 1.0f);
            rendy.materials[3].SetFloat("Vector1_32B55E2", 1.0f);
            hz.Play();
            activated = true;
            turningOn = true;
            timer = 0;
            horizontalValue = 0;
            verticalValue = 0;
            strengthValue = 0;
        }
	}

    void Update()
    {
        TurnOn();
        TurnOff();
    }

    private void OnMouseDown()
    {
        //Turn ON
        if (!activated) {
            rendy.materials[0].SetFloat("Vector1_36F8660A", 1.0f);
            rendy.materials[1].SetFloat("Vector1_A5282C4", 1.0f);
            rendy.materials[3].SetFloat("Vector1_32B55E2", 1.0f);
            hz.Play();
            activated = true;
            turningOn = true;
            timer = 0;
            horizontalValue = 0;
            verticalValue = 0;
            strengthValue = 0;
        }
        //Turn Off
        else if (activated)
        {
            activated = false;
            turningOff = true;
            timer = 0;
            horizontalValue = 1;
            verticalValue = 1;
            strengthValue = 0;
        }
    }

    void TurnOn() {
        if (turningOn) {
            if (timer <= 1.0f) {
                horizontalValue = Mathf.Lerp(horizontalValue, 1.5f, timer);
                verticalValue = Mathf.Lerp(verticalValue, 0.05f, timer);
                strengthValue = Mathf.Lerp(strengthValue, 10, timer);
            }
            else if (timer <= 2.0f) {
                horizontalValue = Mathf.Lerp(horizontalValue, 1, timer);
                verticalValue = Mathf.Lerp(verticalValue, 1, timer - 1);
                strengthValue = Mathf.Lerp(strengthValue, 0, timer - 1);
            }
            else {
                turningOn = false;
            }
            timer += Time.deltaTime / speed;
            rendy.materials[0].SetFloat(horizontalMask, horizontalValue);
            rendy.materials[0].SetFloat(verticalMask, verticalValue);
            rendy.materials[0].SetFloat(strength, strengthValue);
        }
    }

    void TurnOff() {
        if (turningOff) {
            if (timer <= 1.0f) {
                horizontalValue = Mathf.Lerp(horizontalValue, 1.5f, timer);
                verticalValue = Mathf.Lerp(verticalValue, 0.05f, timer);
                strengthValue = Mathf.Lerp(strengthValue, 10, timer);
            }
            else if (timer <= 2.0f) {
                horizontalValue = Mathf.Lerp(horizontalValue, 0, timer);
                verticalValue = Mathf.Lerp(verticalValue, 0, timer - 1);
                strengthValue = Mathf.Lerp(strengthValue, 0, timer - 1);
            }
            else {
                turningOff = false;
                rendy.materials[0].SetFloat("Vector1_36F8660A", 0.0f);
                rendy.materials[1].SetFloat("Vector1_A5282C4", 0.0f);
                rendy.materials[3].SetFloat("Vector1_32B55E2", 0.0f);
                hz.Stop();
            }
            timer += Time.deltaTime / speed;
            rendy.materials[0].SetFloat(horizontalMask, horizontalValue);
            rendy.materials[0].SetFloat(verticalMask, verticalValue);
            rendy.materials[0].SetFloat(strength, strengthValue);
        }
    }
}
