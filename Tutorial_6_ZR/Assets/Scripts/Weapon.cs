using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private ParticleSystem muzzkeFlash;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private TextMeshProUGUI ammoText;
    private EnemyHealth _enemyHealth;
    private bool canShoot = true;

    void OnEnable()
    {
        canShoot = true;
    }
    void Update()
    {
        DisplayAmmo();
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
           StartCoroutine(Shoot()) ;
        }   
    }

    void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }
    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType)>0)
        {
            PlayMuzzkeFkash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
          
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
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
