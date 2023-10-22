using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : Singleton<PathSystem>
{
     private Flag[] _pathFlags;

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _moveSpeed;
    private float _speed;
    private int _currentFlag;
    static private Vector3 _currentPosition;

    private bool _pathfollowing = false;


    private void Update() {
       if(_pathfollowing)
       {
            _speed = Time.deltaTime * _moveSpeed;
            if(_player.transform.position != _currentPosition) // Move player from current cell to the next
            {
                _player.transform.position =  Vector3.MoveTowards( _player.transform.position, _currentPosition, _speed);
                
            }else
            {
                if(_currentFlag < _pathFlags.Length - 1)
                {
                    _currentFlag++;
                    CheckFlag();
                }

               
            }
       }
    }

    public void BeginPath() // For testing
    {
        _pathFlags = GetComponentsInChildren<Flag>();
        CheckFlag();
        _pathfollowing = true;
    }
    private void CheckFlag() // Get the next walking path
    {
       if(_currentFlag < _pathFlags.Length - 1)
        {
           _speed = 0;
        }
         _currentPosition = _pathFlags[_currentFlag].transform.position;
        
    }
}
