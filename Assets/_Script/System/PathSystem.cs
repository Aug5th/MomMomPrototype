using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : Singleton<PathSystem>
{
    private Flag[] _pathFlags;

    [SerializeField]
    private int _currentFlag;
    static private Vector3 _currentPosition;

    private bool _pathfollowing = false;


    private void Update() {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_pathfollowing)
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
    }

    public void BeginPath() // Begin walking
    {
        _pathFlags = GetComponentsInChildren<Flag>();
        CheckFlag();
        _pathfollowing = true;
    }
    private void CheckFlag() // Get the next walking path
    {      
        _currentPosition = _pathFlags[_currentFlag].transform.position;
        
    }
}
