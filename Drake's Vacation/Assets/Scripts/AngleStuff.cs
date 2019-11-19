using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AngleStuff
{
    public static float SignedRoll(float angle)
    {
        angle %= 360;

        while (angle < -180)
        {
            angle += 360;
        }

        if (angle >= 180)
        {
            angle -= 360;
        }

        return angle;
    }
}
