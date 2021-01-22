using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hits = 10;



     ScoreBoard scoreBoard;
    
    void Start()
    {
        NonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void NonTriggerBoxCollider()
    {
        Collider enemyBoxCollider = gameObject.AddComponent<BoxCollider>();
        enemyBoxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hits <=0)
        {
            KillEnemy();
        }
    }

     void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits--;    }

    void KillEnemy()
    {
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);   
    }
}
