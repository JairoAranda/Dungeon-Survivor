using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMultiplier : MonoBehaviour
{
    public static float ScaleFactor(int multiplier, float lvl)
    {
        float scaleFactor = 1f + ((float)(lvl - 1) / 19f) * (multiplier - 1);

        return scaleFactor;
    }
}
