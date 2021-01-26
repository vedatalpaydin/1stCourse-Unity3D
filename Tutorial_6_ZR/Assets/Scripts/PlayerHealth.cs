using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] private float health = 10f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health<=0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Debug.Log("you are dead");
        //Destroy(gameObject);
    }
}
