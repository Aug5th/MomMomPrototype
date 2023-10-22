using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlacementSystem : Singleton<PlacementSystem>
{
    [SerializeField]
    private InputSystem _inputManager;
    [SerializeField]
    private Grid _grid;
    [SerializeField]
    private Tilemap _drawMap;
    [SerializeField]
    private Tilemap _floorMap;
    [SerializeField]
    private Tilemap _objectMap;
    [SerializeField]
    private Tile _path;
    [SerializeField]
    private GameObject _flag;
    [SerializeField]
    private LayerMask _pathLayermask;
    [SerializeField] 
    private PathSystem _pathfollower; // For testing Kid walking

    private Vector3 _touchPosition;
    private Vector3 _cellCenterPosition;
    private Vector3Int _gridPosition;
    private Vector3Int _lastGridPosition;
    private Vector2 _currentPosition;
    private bool _isDrawingPath = false;
    private bool _isValidPath = true;
    private Color _pathColor;

    private void Update()
    {
        PathPlacement();
    }

    private void PathPlacement()
    {
        if(_inputManager.IsScreenTouched()) // If have touch, get cell and draw path
        {
            _touchPosition = _inputManager.getCurrentTouchPosition();
            _gridPosition = _grid.WorldToCell(_touchPosition);
            _cellCenterPosition = _grid.GetCellCenterWorld(_gridPosition);
            if(_inputManager.IsTouchOnLayer(_pathLayermask))
            {
                if(!_isDrawingPath)
                {
                    _isDrawingPath = true;
                    //Clear all paths and flags when player draw new one
                    RemovePath();
                }
                DrawPath();
            }
        }
        else
        {   
            if(_isValidPath)
            {
                _isDrawingPath = false;
                _pathColor = _drawMap.GetComponent<Tilemap>().color;
                _pathColor.a = 1f;
                _drawMap.GetComponent<Tilemap>().color = _pathColor;
            }
            else
            {
                RemovePath();
                _isValidPath = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space)) // For testing Kid walking
        {
            _pathfollower.BeginPath();
        }
    }

    private void DrawPath()
    {
        Vector2 newPosition = _cellCenterPosition;
        if(_currentPosition != newPosition) // Dont draw on the same tile more than once
        {
            _pathColor = _drawMap.GetComponent<Tilemap>().color; 
           
            if(_floorMap.GetTile(_gridPosition) != null) // Check if there's a floor here
            {
                if(_drawMap.GetTile(_gridPosition)!= null) // If there's already a path here
                {
                    _drawMap.SetTileFlags(_gridPosition, TileFlags.None);
                    _drawMap.SetColor(_gridPosition, Color.black);
                    _isValidPath = false;
                }
                else
                {
                    _drawMap.SetTile(_gridPosition,_path);
                    _currentPosition = newPosition;
                    GameObject nextFlag = Instantiate(_flag, _currentPosition, Quaternion.identity);
                    nextFlag.transform.parent = _drawMap.transform;
                    if(_objectMap.GetTile(_gridPosition) != null) // If CollisionObjects has tile in it
                    {
                        _drawMap.SetTileFlags(_gridPosition, TileFlags.None);
                        _drawMap.SetColor(_gridPosition, Color.black);
                        _isValidPath = false;
                    }
                }
            }
            _lastGridPosition = _gridPosition;
            _pathColor.a = 0.5f;
            _drawMap.GetComponent<Tilemap>().color = _pathColor;
        }
    }

    private void RemovePath()
    {
         _drawMap.ClearAllTiles();

        Object[] allObjects = FindObjectsOfType(typeof(GameObject));
            foreach(GameObject obj in allObjects) {
                if(obj.tag == "Flag")
                {
                    Destroy(obj);
                }
            }
    }

    private bool IsValidPath()
    {
       // TO DO
       return true;
    }
}
