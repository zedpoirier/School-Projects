using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public bool hasSeen = false;
    public int textCount = 0;
    public int activateCount = 0;
    public int hintCount = 6;
    public int deathCount = 50;
    public Text text;
    public Image img;
    public Collider trigger;
    public string[] lines;

    private void Start()
    {
        trigger.enabled = false;
    }

    private void Update()
    {
        if (textCount == activateCount)
        {
            trigger.enabled = true;
        }

        if (textCount == hintCount)
        {
            img.enabled = true;
        }

        if (textCount == deathCount)
        {
            Application.OpenURL("https://www.youtube.com/watch?v=oHg5SJYRHA0");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void NextText()
    {
        if (hasSeen)
        {
            if (textCount >= lines.Length)
            {
                text.text = "";
            }
            else
            {
                text.text = lines[textCount];
                textCount++;
            }
        }
        hasSeen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hasSeen = true;
        }
    }
}
