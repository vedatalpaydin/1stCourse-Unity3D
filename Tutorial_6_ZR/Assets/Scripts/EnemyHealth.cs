using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    private Animator anim;
    private bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        health -= damage;
        if (health<=0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        if (isDead) return;
        isDead = true;
        anim.SetTrigger("death");
         
    }
}
