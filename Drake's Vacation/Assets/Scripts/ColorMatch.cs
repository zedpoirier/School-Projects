using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMatch : MonoBehaviour
{
    public bool isColorMismatched;
    public Light colorToMatch;
    public MeshRenderer colorToUpdate;

    void Update()
    {
        isColorMismatched = colorToMatch.color != colorToUpdate.material.GetColor("_EmissionColor");
        if (isColorMismatched)
        {
            colorToUpdate.material.SetColor("_EmissionColor", colorToMatch.color);
        }
    }
}
