using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private int towerLimit = 5;
    [SerializeField] Tower turretPrefab;
    [SerializeField]  Transform towerParent;
    private Queue<Tower> towerQueue = new Queue<Tower>();
    public void AddTower(Waypoint baseWaypoint)
    {
        int numTower = towerQueue.Count;
        if (numTower < towerLimit)
        {
            InstantiateNewTowe(baseWaypoint);
        }
        else
        {
            MoveTower(baseWaypoint);
        }
    }

    void InstantiateNewTowe( Waypoint baseWaypoint)
    {
        var newTower=Instantiate(turretPrefab,baseWaypoint.transform.position,Quaternion.identity);
        newTower.transform.parent = towerParent;
        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;
        towerQueue.Enqueue(newTower);
    }
    void MoveTower(Waypoint newBaseWaypoint)
    {
        var oldTower= towerQueue.Dequeue();
        oldTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;
        oldTower.baseWaypoint = newBaseWaypoint;
        oldTower.transform.position = newBaseWaypoint.transform.position;
        towerQueue.Enqueue(oldTower);
    }
}
