using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Range
{
    public int Maximum;
    public int Minimum;

    public Range (int min, int max)
    {
        Minimum = min;
        Maximum = max;
    }
}
