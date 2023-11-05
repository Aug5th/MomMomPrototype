using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : Singleton<PathSystem>
{
    private Flag[] _pathFlags;
    private Flag[] _NomNomFlags;
    private Flag[] _TeddyFlags;

    [SerializeField]
    private int _currentFlag;
    private int _currentNomNomFlag;
    private int _currentTeddyFlag;
    static private Vector3 _currentPosition;
    static private Vector3 _currentNomNomPosition;
    static private Vector3 _currentTeddyPosition;
    private bool _isUseSkill = false;
    private bool _isTeddyWalking = false;
    public bool PathFollowing = false;


    private void Update() {
        MovePlayer();
        MoveNomNom();
        MoveTeddy();
    }

    private void MovePlayer()
    {
        if (PathFollowing && !_isUseSkill && _isTeddyWalking)
        {
            if (Kid.Instance.transform.position != _currentPosition)
            {
                Kid.Instance.Move(_currentPosition);
            }
            else
            {
                if (_currentFlag < _pathFlags.Length - 1)
                {
                    _currentFlag++;
                    CheckFlag();
                }
            }

            if (_currentFlag == _pathFlags.Length - 1)
            {
                Kid.Instance.StopMoving();
            }
        }
        else
        {
            Kid.Instance.StopMoving();
        }
    }

     private void MoveTeddy()
    {
        if (PathFollowing && !_isUseSkill)
        {
            if (Teddy.Instance.transform.position != _currentTeddyPosition)
            {
                Teddy.Instance.Move(_currentTeddyPosition);
            }
            else
            {
                if (_currentTeddyFlag < _TeddyFlags.Length - 1)
                {
                    _currentTeddyFlag++;
                    CheckFlag();
                }
            }

            if (_currentTeddyFlag == _TeddyFlags.Length - 1)
            {
                Teddy.Instance.StopMoving();
            }

            if(_currentTeddyFlag > 1)
            {
                _isTeddyWalking = true;
            }
        }
        else
        {
            Teddy.Instance.StopMoving();
        }
    }

    private void MoveNomNom()
    {
        if(!EnemySpawnTrigger.Instance.EnemySpawning)
        {
            return;
        }

        //Debug.Log("Path Following = " + PathFollowing);
        if (PathFollowing)
        {
            if (NomNom.Instance.transform.position != _currentNomNomPosition)
            {
                NomNom.Instance.Move(_currentNomNomPosition);
            }
            else
            {
                if (_currentNomNomFlag < _NomNomFlags.Length - 1)
                {
                    _currentNomNomFlag++;
                    CheckFlag();
                }
            }

            if (_currentNomNomFlag == _NomNomFlags.Length - 1)
            {
                NomNom.Instance.StopMoving();
            }
        }
        else
        {
            NomNom.Instance.StopMoving();
        }
    }

    public void StandStill(bool standstill)
    {
        if(standstill)
        {
            _isUseSkill = true;
        }
        else
        {
            _isUseSkill = false;
        }
    }

    public void BeginPath() // Begin walking
    {
        _pathFlags = GetComponentsInChildren<Flag>();
        _NomNomFlags = GetComponentsInChildren<Flag>();
        _TeddyFlags = GetComponentsInChildren<Flag>();
        CheckFlag();
        PathFollowing = true;
    }
    private void CheckFlag() // Get the next walking path
    {      
        _currentPosition = _pathFlags[_currentFlag].transform.position;
        _currentNomNomPosition = _NomNomFlags[_currentNomNomFlag].transform.position;
        _currentTeddyPosition = _TeddyFlags[_currentTeddyFlag].transform.position;
    }
}
