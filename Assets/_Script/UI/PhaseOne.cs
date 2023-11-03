using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseOne : MonoBehaviour
{
    public void StartPhaseTwo()
    {
        GameManager.Instance.UpdateGameState(GameState.PhaseTwo);
    }

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

    public void Pause()
    {
        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame();
    }
}
