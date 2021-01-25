using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemy;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private AudioClip spawnSFX;
    [Range(0.1f,120f)] [SerializeField] float secondsBetweenSpawns=5f;
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (true) 
        {
            var newEnemy=Instantiate(enemy,transform.position, Quaternion.identity);
            GetComponent<AudioSource>().PlayOneShot(spawnSFX);
            newEnemy.transform.parent = enemyParent;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
