using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PhaseTwo : Singleton<PhaseTwo>
{
    private bool _isAttacking = false;
    private bool _isFixing = false;

    [SerializeField] private GameObject _attackActiveImg;
    [SerializeField] private GameObject _attackCancelImg;
    [SerializeField] private GameObject _fixingActiveImg;
    [SerializeField] private GameObject _fixingCancelImg;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _attackActiveImg.SetActive(true);
        _attackCancelImg.SetActive(false);
        _fixingActiveImg.SetActive(true);
        _fixingCancelImg.SetActive(false);
    }

    public void AttackActiveImagePress()
    {
        if(!EnemySpawnTrigger.Instance.EnemySpawning || _isFixing)
        {
            return;
        }
        SetAttackMode();
        _attackActiveImg.SetActive(false);
        _attackCancelImg.SetActive(true);
    }
    public void AttackCancelImagePress()
    {
        StopAttackMode();
        _attackActiveImg.SetActive(true);
        _attackCancelImg.SetActive(false);
    }
    public void FixingActiveImagePress()
    {
        if (!EnemySpawnTrigger.Instance.EnemySpawning || _isAttacking)
        {
            return;
        }
        SetHealingMode();
        _fixingActiveImg.SetActive(false);
        _fixingCancelImg.SetActive(true);
    }
    public void FixingCancelImagePress()
    {
        SetHealingMode();
        _fixingActiveImg.SetActive(true);
        _fixingCancelImg.SetActive(false);
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
        if (toys.Length > 0)
        {
            foreach (var toy in toys)
            {
                var toySetting = toy.GetComponent<Toy>();
                if (_isFixing)
                {
                    // Toys chase monsters
                    toySetting.SetHealingMode(false);
                    //PathSystem.Instance.StandStill(false); // Kid keep moving
                    Teddy.Instance.MoveSpeed = Teddy.Instance.NormalSpeed;
                    Kid.Instance.MoveSpeed = Kid.Instance.NormalSpeed;
                    //buttonAttack.interactable = true;
                }
                else
                {
                    // Toys go to kid
                    toySetting.SetHealingMode(true);
                    //PathSystem.Instance.StandStill(true); // Kid stop moving
                    Teddy.Instance.MoveSpeed = Teddy.Instance.SlowSpeed;
                    Kid.Instance.MoveSpeed = Kid.Instance.SlowSpeed;
                    //buttonAttack.interactable = false;
                }
            }

            if (_isFixing) // If skill is used, stop using
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
        if (_isAttacking) // If skill is used, stop using
        {
            return;
        }

        Teddy.Instance.IsAttackMode = true;
        Teddy.Instance.MoveSpeed = Teddy.Instance.SlowSpeed;
        Kid.Instance.MoveSpeed = Kid.Instance.SlowSpeed;
        _isFixing = false;
        PathSystem.Instance.StandStill(false);

        StartCoroutine(AttackModeTimer());
        _isAttacking = true;

    }

    public void StopAttackMode()
    {
        Teddy.Instance.IsAttackMode = false;
        Teddy.Instance.MoveSpeed = Teddy.Instance.NormalSpeed;
        Kid.Instance.MoveSpeed = Kid.Instance.NormalSpeed;
        _isAttacking = false;
    }

    private IEnumerator AttackModeTimer()
    {
        yield return new WaitForSeconds(10f);
        if(_isAttacking)
        {
            Teddy.Instance.IsAttackMode = false;
            Teddy.Instance.MoveSpeed = 1f;
            Kid.Instance.MoveSpeed = 1f;
            _isAttacking = false;
        }
    }

}

