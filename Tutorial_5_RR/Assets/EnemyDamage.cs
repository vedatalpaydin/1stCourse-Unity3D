 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]  ParticleSystem hitParticle;
    [SerializeField]  ParticleSystem deathParticle;
    [SerializeField]  AudioClip hitSFX;
    [SerializeField]  AudioClip deathSFX;
    private AudioSource audio;
    private int hitPoint = 10;
    void Start()
    {
        audio = GetComponent<AudioSource>();
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
        audio.PlayOneShot(hitSFX);
        hitParticle.Play();
        hitPoint--;
    } 
    private void KillEnemy()
    {
       var vFX= Instantiate(deathParticle,transform.position, Quaternion.identity);
       vFX.Play();
       audio.Stop();;
   AudioSource.PlayClipAtPoint(deathSFX,Camera.main.transform.position);
       float destroyDelay = vFX.main.duration;
        Destroy(vFX.gameObject,destroyDelay);
        Destroy(gameObject);
    }
}
