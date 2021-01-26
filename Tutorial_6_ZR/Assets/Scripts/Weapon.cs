using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float range = 100f;
    [SerializeField] private ParticleSystem muzzkeFlash;
    [SerializeField] private GameObject hitVFX;
    
    private float damage = 1f;
    private EnemyHealth _enemyHealth;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }   
    }

    void Shoot()
    {
        PlayMuzzkeFkash();
        ProcessRaycast();
    }

    void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range))
        {
            PlayHitVFX(hit);
            _enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (_enemyHealth==null) return;
            _enemyHealth.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void PlayHitVFX(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact,0.1f);
    }

    void PlayMuzzkeFkash()
     {
         muzzkeFlash.Play();
     }
}
