using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PhaseTwo : MonoBehaviour
{
    public void ClearPath()
    {
        GridSystem.Instance.ClearPath();
    }

    public void BeginWalking()
    {
        // Stop draw path, begin walking
        if(GridSystem.Instance.IsEndPointReach())
        {
            GridSystem.Instance.HidePath(true);
            GridSystem.Instance.SetPlacementMode(false);
            PathSystem.Instance.BeginPath();
        }
    } 
}
