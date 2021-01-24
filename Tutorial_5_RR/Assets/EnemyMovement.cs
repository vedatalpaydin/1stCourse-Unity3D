using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path=pathFinder.GetPath();
        StartCoroutine(MoveTheEnemy(path));
    }

    IEnumerator MoveTheEnemy(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
        
    void Update()
    {
        
    }
}
