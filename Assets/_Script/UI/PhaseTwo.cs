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
        if(GridSystem.Instance.IsEndPointReach())
        {
            // Stop draw path, begin walking
            GridSystem.Instance.SetPlacementMode(false);
            PathSystem.Instance.BeginPath();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Reumse()
    {
        Time.timeScale = 1f;
    }
}
