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

    private bool _isAttacking;
    private bool _isFixing;

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
        _isFixing = false;
        _isAttacking = false;
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
                if(_isFixing)
                {
                    // Toys chase monsters
                    toySetting.SetHealingMode(false);
                    PathSystem.Instance.StandStill(false); // Kid keep moving
                    buttonAttack.interactable = true;
                }
                else
                {
                    // Toys go to kid
                    toySetting.SetHealingMode(true);
                    PathSystem.Instance.StandStill(true); // Kid stop moving
                    buttonAttack.interactable = false;
                }
            }

            if(_isFixing) // If skill is used, stop using
            {
                _isFixing = false;
            }
            else
            {
                _isFixing = true;
                _isAttacking = false;
            }
        }
    }

    public void SetAttackMode()
    {
        if(_isAttacking) // If skill is used, stop using
        {
           return;
        }
       
        Teddy.Instance.IsAttackMode = true;
        Teddy.Instance.MoveSpeed = 0.5f;
        Kid.Instance.MoveSpeed = 0.5f;
        _isFixing = false;
        PathSystem.Instance.StandStill(false);
        buttonFix.interactable = false;
        buttonAttack.interactable = false;

        StartCoroutine(AttackModeTimer());
        _isAttacking = true;
    
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

     private IEnumerator AttackModeTimer()
    {
        yield return new WaitForSeconds(10f);
        Teddy.Instance.IsAttackMode = false;
        Teddy.Instance.MoveSpeed = 1f;
        Kid.Instance.MoveSpeed = 1f;
        _isAttacking = false;
        buttonAttack.interactable = true;
        buttonFix.interactable = true;
    }
}
