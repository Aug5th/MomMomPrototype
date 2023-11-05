using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseOne : Singleton<PhaseOne>
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonMove;
    [SerializeField] private Button buttonFix;
    [SerializeField] private Button buttonAttack;

    public void StartPhaseTwo()
    {
        if (GridSystem.Instance.IsEndPointReach())
        {
            GridSystem.Instance.HidePath(true);
            GridSystem.Instance.SetPlacementMode(false);
            PathSystem.Instance.BeginPath();
            GameManager.Instance.UpdateGameState(GameState.PhaseTwo);
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        SetButtonsInteractable(false);
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

    public void SetButtonsInteractable(bool interactable)
    {
        buttonPlay.interactable = interactable;
        buttonMove.interactable = interactable;
    }


}
