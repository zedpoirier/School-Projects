using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class UIManager : MonoBehaviour
{
    [Header("PP Volume")]
    public PostProcessVolume volume;
    Bloom bloom;
    DepthOfField dof;
    ColorGrading grading;

    [Header("Button")]
    public Button button;
    public bool btnState = true;

    [Header("Slider")]
    public Slider slider;
    public GameObject[] spooks; //make you more spook then the slider can access!!!!
    public int count;

    [Header("Dropdown")]
    public Dropdown dropdown;
    public float midTemp = 0;
    public float midTint = 0;
    public float coolTemp = 0;
    public float coolTint = 0;
    public float hotTemp = 0;
    public float hotTint = 0;

    private void Start()
    {
        volume.profile.TryGetSettings(out bloom);
        volume.profile.TryGetSettings(out dof);
        volume.profile.TryGetSettings(out grading);
    }

#if !UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
#endif

    public void ToggleDistortion()
    {
        dof.active = !dof.active;
        bloom.active = !bloom.active;
    }

    public void RandomizeSpooks()
    {
        for (int i = 0; i < spooks.Length; i++)
        {
            spooks[i].SetActive(false);
        }
        count = (int)slider.value;
        while (count > 0)
        {
            for (int i = 0; i < spooks.Length; i++)
            {
                int rand = Random.Range(0, 2);
                if (rand == 1)
                {
                    spooks[i].SetActive(true);
                    count--;
                }
                if (count <= 0) break;
            }
        }
    }

    public void ChangeColorTemperature()
    {
        switch (dropdown.value)
        {
            case 0: // mid
                grading.temperature.value = midTemp;
                grading.tint.value = midTint;
                break;
            case 1: // cool
                grading.temperature.value = coolTemp;
                grading.tint.value = coolTint;
                break;
            case 2: // hot
                grading.temperature.value = hotTemp;
                grading.tint.value = hotTint;
                break;
            default:
                break;
        }
    }
}
