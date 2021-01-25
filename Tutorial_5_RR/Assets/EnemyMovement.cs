using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticle;
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
            yield return new WaitForSeconds(.5f);
        }
        var vFX= Instantiate(deathParticle,transform.position, Quaternion.identity);
        vFX.Play();
        float destroyDelay = vFX.main.duration;
        Destroy(vFX.gameObject,destroyDelay);
        Destroy(gameObject);
    }
        
    void Update()
    {
        
    }
}
