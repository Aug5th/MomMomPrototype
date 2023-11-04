using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PhaseTwo : Singleton<PhaseTwo>
{
     private bool _isSkillUsed = false;
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
}
