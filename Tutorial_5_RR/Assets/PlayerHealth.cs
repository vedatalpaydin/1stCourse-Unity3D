using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int healt = 10;
    [SerializeField] int decreaseHealth = 1;
    [SerializeField] private AudioClip selfDistructSFX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

  void  OnTriggerEnter(Collider other)
  {
      GetComponent<AudioSource>().PlayOneShot(selfDistructSFX);
      healt = healt - decreaseHealth;
  }
}
