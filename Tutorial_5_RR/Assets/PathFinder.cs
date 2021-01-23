using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    private bool isRunning = true;
    
    Vector2Int[] directions = new Vector2Int[]
    {
    Vector2Int.up, 
    Vector2Int.right, 
    Vector2Int.down, 
    Vector2Int.left, 
    };
    void Start()
    {
        LoadBlocks();
        StartAndEndColor();
        Pathfind();
        //ExploreNeighBour();

    }

    private void Pathfind()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count>0 && isRunning)
        {
            var searchCenter = queue.Dequeue();
            print("Searching from: "+ searchCenter);
            HaltIfEndFound(searchCenter);
            ExploreNeighbours(searchCenter);
            searchCenter.isExplored = true;
        }
        print("Finished Pathfinding");
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if (searchCenter==endWaypoint)
        {
            print("Searching from end node, therefore stopping");
            isRunning = false;
        }
    }

    private void ExploreNeighbours(Waypoint from)
    {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = from.GetGridPos() + direction;
            try
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
            catch
            {
               // do nothing
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour= grid[neighbourCoordinates];
        if (neighbour.isExplored)
        {
            // do nothing
        }
        else
        {
            neighbour.SetTopColor(Color.blue);
            queue.Enqueue(neighbour);
            print("Queueing " +neighbour);
        }
    }

    private void StartAndEndColor()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    void LoadBlocks()
    {
         var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping Overlapping Block: "+waypoint);
            }
            else
            {
                grid.Add(gridPos,waypoint);
            }
        }
    }
}
