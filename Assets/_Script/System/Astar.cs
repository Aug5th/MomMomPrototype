using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class Astar
{
    private Spot[,] _Spots;

    public Astar(Vector3Int[,] grid, int columns, int rows)
    {
        _Spots = new Spot[columns,rows];
    }

    private bool IsValidPath(Vector3Int[,] grid, Spot start, Spot end)
    {
        if (end == null)
        {
             return false;
        }
        if (start == null)
        {
             return false;
        }
        if (end.isUsed >= 1) // the destination is already had path
        {
             return false;
        }
        return true;
    }

    public List<Spot> CreatePath(Vector3Int[,] grid, Vector2Int start, Vector2Int end, int length)
    {
        Spot StartPoint = null;
        Spot EndPoint = null;
        int columns = _Spots.GetUpperBound(0) + 1;
        int rows = _Spots.GetUpperBound(1) + 1;
        _Spots = new Spot[columns,rows];

        for(int i = 0; i < columns; i++) // Create all draw spots based on created grid
        {
            for(int j = 0; j < rows; j++)
            {
                _Spots[i,j] = new Spot(grid[i,j].x, grid[i,j].y, grid[i,j].z);
            }
        }

        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                _Spots[i,j].AddNeighbors(_Spots,i,j); // Add 4 other spots which this one connect to

                if(_Spots[i,j].X == start.x && _Spots[i,j].Y == start.y)
                {
                    StartPoint = _Spots[i,j];
                }
                if(_Spots[i,j].X == end.x && _Spots[i,j].Y == end.y)
                {
                    EndPoint = _Spots[i,j];
                }
            }
        }

        if(!IsValidPath(grid,StartPoint,EndPoint))
        {
            return null;
        }

        List<Spot> OpenSet = new List<Spot>(); // Spots in this set can be used for path
        List<Spot> ClosedSet = new List<Spot>(); // Spots in this set are already used
        OpenSet.Add(StartPoint);

        while(OpenSet.Count > 0)
        {
            int winner = 0;
            for(int i = 0; i < OpenSet.Count; i++) // Find the shortest distance within the OpenSet
            {
                if(OpenSet[i].F < OpenSet[winner].F)
                {
                    winner = i;
                }
                else if (OpenSet[i].F == OpenSet[winner].F)
                {
                    if(OpenSet[i].H < OpenSet[winner].H)
                    {
                        winner = i;
                    }
                }
            }

            Spot currentSpot = OpenSet[winner];

            // If the path reach the destination
            if(EndPoint != null && OpenSet[winner] == EndPoint)
            {
                List<Spot> Path = new List<Spot>();
                Spot temp = currentSpot;
                Path.Add(temp);
                while(temp.previous != null)
                {
                    Path.Add(temp.previous);
                    temp = temp.previous;
                }
                if(length  - (Path.Count - 1) < 0)
                {
                    Path.RemoveRange(0, (Path.Count - 1) - length);
                }
                return Path;
            }

            OpenSet.Remove(currentSpot);
            ClosedSet.Add(currentSpot);

            // If not reach the destination, add Spot's Neighbors to OpenSet for next shortest spot find
            List<Spot> neighbors = currentSpot.Neighbors;
            for(int i = 0; i < neighbors.Count; i++)
            {
                Spot nextSpot  = neighbors[i];
                if(!ClosedSet.Contains(nextSpot) && nextSpot.isUsed < 1) // check if spot is in ClosedSet or not
                {
                    int tempG = currentSpot.G + 1;
                    bool newPath = false;
                    if(OpenSet.Contains(nextSpot)) // If this spot is already in OpenSet, check if it the shortest direction
                    {
                        if(tempG < nextSpot.G)
                        {
                            nextSpot.G = tempG;
                            newPath = true;
                        }
                    }
                    else // This spot is new, add it to OpenSet to use
                    {
                        nextSpot.G = tempG;
                        newPath = true;
                        OpenSet.Add(nextSpot);
                    }
                    if(newPath) // If there's new spot, calculate the F and H of that spot
                    {
                        nextSpot.H = Heuristic(nextSpot,EndPoint);
                        nextSpot.F = nextSpot.G + nextSpot.H;
                        nextSpot.previous = currentSpot;
                    }
                }
            }
        }

        return null;
    }

    private int Heuristic(Spot a, Spot b)
    {
        int dx = Math.Abs(a.X - b.X);
        int dy = Math.Abs(a.Y - b.Y);

        return 1 * (dx + dy);
    }
}

public class Spot 
{
    public int X;
    public int Y;
    public int F;
    public int G;
    public int H;
    public int isUsed = 0;
    public List<Spot> Neighbors;
    public Spot previous = null;

    public Spot(int x, int y, int used)
    {
        X = x;
        Y = y;
        F = 0;
        G = 0;
        H = 0;
        Neighbors = new List<Spot>();
        isUsed = used;
    }
    
    public void AddNeighbors(Spot[,] grid, int x, int y)
    {
        if (x < grid.GetUpperBound(0))
        {
            Neighbors.Add(grid[x + 1, y]);
        }
        if (x > 0)
        {
            Neighbors.Add(grid[x - 1, y]);
        }
        if (y < grid.GetUpperBound(1))
        {
            Neighbors.Add(grid[x, y + 1]);
        }
        if (y > 0)
        {
            Neighbors.Add(grid[x, y - 1]);
        }
    }
}
