using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile", menuName = "Scriptable Object / Tile")]
public class TileScriptableObject : ScriptableObject
{
    [SerializeField]
    private TileBase _tile;
    public TileBase tile => _tile;
    public TileType TileType;
}

public enum TileType
{
   floor = 0,
   wall = 1,
   checkpoint = 2,
   endpoint = 3
}
