using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] private float health = 10f;
    private DeathHandler _deathHandler;

    void Start()
    {
        _deathHandler = GetComponent<DeathHandler>();
    }
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
        _deathHandler.DeathSceneLoader();
        Debug.Log("you are dead");
    }
}
