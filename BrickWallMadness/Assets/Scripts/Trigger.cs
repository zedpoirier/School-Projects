using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public bool triggered = false;
    public bool simpleTrigger = false;
    public Color disabledColor = Color.red;
    public Color enabledColor = Color.green;

    [Header("Sub-Objects")]
    public Light triggerLight;
    public MeshRenderer buttonRenderer;

    [Header("Player Values")]
    public Transform player;
    public float distance;
    public float maxDistance = 3f;

    [Header("Outline Shader Values")]
    public float minHighlight = 1.0f; //No highlight
    public float maxHighlight = 1.2f; //Visible Highlight

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        distance = Vector3.Distance(player.position, buttonRenderer.transform.position);
    }

    void OnMouseExit()
    {
        Highlight(minHighlight);
    }

    void OnMouseOver()
    {
        if (distance <= maxDistance)
        {
            Highlight(maxHighlight);
            if (Input.GetMouseButtonDown(0))
            {
                triggered = !triggered;
                if (triggered)
                {
                    buttonRenderer.material.color = enabledColor;
                    triggerLight.color = enabledColor;
                }
                else
                {
                    buttonRenderer.material.color = disabledColor;
                    triggerLight.color = disabledColor;
                }
            }
        }
    }

    void Highlight(float value)
    {
        buttonRenderer.material.SetFloat("_OutlineWidth", value);
    }
}
