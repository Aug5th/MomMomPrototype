using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static Vector3 RandomPositionInCircle(Vector3 center , int radius)
    {
        return center + (Vector3)(Random.insideUnitCircle * radius);
    }
}

