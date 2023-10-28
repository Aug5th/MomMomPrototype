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
        GridSystem.Instance.SetPlacementMode(false);
        PathSystem.Instance.BeginPath();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
