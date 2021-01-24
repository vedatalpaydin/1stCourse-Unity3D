 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private int hitPoint = 10;
    void Start()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoint<=0)
        {
            KillEnemy();
        }
    }
    private void ProcessHit()
    {
        hitPoint--;
    } 
    private void KillEnemy()
         {
             Destroy(gameObject);
         }
}
