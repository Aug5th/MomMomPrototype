using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem : Singleton<GridSystem>
{
    [SerializeField]
    private Tilemap _floorMap;
    [SerializeField]
    private Tilemap _pathMap;
    [SerializeField]
    private Tilemap _objectMap;
    [SerializeField]
    private Tilemap _wallMap;
    [SerializeField]
    private TileBase _roadTile;
    [SerializeField]
    private GameObject _flag;
    [SerializeField]
    private Grid _grid;
    private Vector3Int[,] _spots;
    private Astar _astar;
    private List<Spot> _roadPath = new List<Spot>();
    private Camera _camera;
    private BoundsInt _bounds;
    private Vector2Int _startPoint;
    private bool _isPlacementMode = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _floorMap.CompressBounds();
        _pathMap.CompressBounds();
        _objectMap.CompressBounds();
        _bounds = _floorMap.cellBounds;
        _camera = Camera.main;
        // Starting point is where the kid is
        GameObject kid = GameObject.FindGameObjectWithTag("Kid"); 
        Vector3Int gridPos = _floorMap.WorldToCell(kid.transform.position);
        _startPoint = new Vector2Int(gridPos.x, gridPos.y);
      

        CreateGrid();
        _astar = new Astar(_spots, _bounds.size.x, _bounds.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(_isPlacementMode) // Only draw path if the kid not moving
            {
                CreateGrid();
                Vector3 worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPosition = _floorMap.WorldToCell(worldPosition);
                TileBase currentTile = _floorMap.GetTile(gridPosition);
                TileBase checkPoint =  ResourceSystem.Instance.GetTile(TileType.checkpoint);
                if(currentTile != checkPoint) // Only draw path if destination is a checkpoint
                {
                    return;
                }

                if(_roadPath != null && _roadPath.Count > 0)
                {
                    _roadPath.Clear();
                }

                _roadPath = _astar.CreatePath(_spots, _startPoint, new Vector2Int(gridPosition.x, gridPosition.y), 1000);
                if(_roadPath == null)
                {
                    return;
                }

                DrawPath();
                _startPoint = new Vector2Int(_roadPath[0].X, _roadPath[0].Y);
            }
        }
    }

    public void SetPlacementMode(bool mode)
    {
        _isPlacementMode = mode;
    }
    public void ClearPath()
    {
        // Clear all path and flags
        _pathMap.ClearAllTiles();

        Object[] allObjects = FindObjectsOfType(typeof(GameObject));
        foreach(GameObject obj in allObjects) 
        {
            if(obj.tag == "Flag")
            {
                Destroy(obj);
            }
        }

        // Reset starting point back to kid
        GameObject kid = GameObject.FindGameObjectWithTag("Kid");
        Vector3Int gridPos = _floorMap.WorldToCell(kid.transform.position);
        _startPoint = new Vector2Int(gridPos.x, gridPos.y);
    }

    private void CreateGrid()
    {
        _spots = new Vector3Int[_bounds.size.x, _bounds.size.y];
        for(int x = _bounds.xMin, i = 0; i < (_bounds.size.x); x++, i++)
        {
            for(int y = _bounds.yMin, j = 0; j < (_bounds.size.y); y++, j++)
            {   
                // Check if there's floor to draw path and there's no obstacle or wall
                if(_floorMap.HasTile(new Vector3Int(x,y,0))
                   && !_pathMap.HasTile(new Vector3Int(x,y,0))
                   && !_objectMap.HasTile(new Vector3Int(x,y,0))
                   && !_wallMap.HasTile(new Vector3Int(x,y,0))
                    )
                {
                    _spots[i,j] = new Vector3Int(x,y,0);
                }
                else
                {
                    _spots[i,j] = new Vector3Int(x,y,1);
                }
            }
        }
    }

    private void DrawPath()
    {
        for(int i = 0; i < _roadPath.Count; i++)
        {   
            _pathMap.SetTile(new Vector3Int(_roadPath[i].X, _roadPath[i].Y, 0), _roadTile);
        }

        for(int i = _roadPath.Count - 1; i >=0; i--)
        {
            Vector3Int _gridPosition = new Vector3Int(_roadPath[i].X,_roadPath[i].Y,0);
            Vector2 _cellCenterPosition = _grid.GetCellCenterWorld(_gridPosition);
             GameObject nextFlag = Instantiate(_flag, _cellCenterPosition, Quaternion.identity);
                nextFlag.transform.parent = _grid.transform;
        }
    }
}
