using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProgressData {
    public int completed_levels;
    public bool[,] picked_flowers = new bool[10, 3];
}
