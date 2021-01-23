using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private List<Waypoint> _waypoints;
    void Start()
    {
        StartCoroutine(MoveTheEnemy());
    }

    IEnumerator MoveTheEnemy()
    {
        foreach (Waypoint waypoint in _waypoints)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
        
    void Update()
    {
        
    }
}
