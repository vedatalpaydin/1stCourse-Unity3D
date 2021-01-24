using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LookTarget : MonoBehaviour
{
    [SerializeField] private Transform LookForPan; 
    Transform enemy;

    [SerializeField] ParticleSystem projectile;

    private float attackRange = 30f;

    void Update()
    {
        SetTargetEnemy();
        if (enemy)
        {
            LookForPan.LookAt(enemy);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length ==0) { return;}

        Transform closestEnemy = sceneEnemies[0].transform;
        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        enemy = closestEnemy;

    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var disToA = Vector3.Distance(transform.position, transformA.position);
        var disToB = Vector3.Distance(transform.position, transformB.position);
        if (disToA<disToB)
        {
            return transformA;
        }

        return transformB;
    }

    void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(enemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <=attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emmisionModule = projectile.emission;
        emmisionModule.enabled = isActive;
    }
}
