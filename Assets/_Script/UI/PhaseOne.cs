using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseOne : Singleton<PhaseOne>
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonMove;

    private bool _isSkillUsed;

    public void StartPhaseTwo()
    {
        //if (GridSystem.Instance.IsEndPointReach())
        {
            GridSystem.Instance.HidePath(true);
            GridSystem.Instance.SetPlacementMode(false);
            PathSystem.Instance.BeginPath();
            GameManager.Instance.UpdateGameState(GameState.PhaseTwo);
        }
    }

    protected override void LoadComponents()
    {
        _isSkillUsed = false;
        base.LoadComponents();
        SetButtonsInteractable(false);
        buttonPlay.interactable = true;
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

    public void SetHealingMode() // Set healing mode
    {
        GameObject[] toys = GameObject.FindGameObjectsWithTag("Toy");
        if(toys.Length > 0)
        {
            foreach(var toy in toys)
            {
                var toySetting = toy.GetComponent<Toy>();
                if(_isSkillUsed)
                {
                    // Toys chase monsters
                    toySetting.SetHealingMode(false);
                    PathSystem.Instance.StandStill(false); // Kid keep moving
                }
                else
                {
                    // Toys go to kid
                    toySetting.SetHealingMode(true);
                    PathSystem.Instance.StandStill(true); // Kid stop moving
                }
            }

            if(_isSkillUsed) // If skill is used, stop using
            {
                _isSkillUsed = false;
            }
            else
            {
                _isSkillUsed = true;
            }
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
