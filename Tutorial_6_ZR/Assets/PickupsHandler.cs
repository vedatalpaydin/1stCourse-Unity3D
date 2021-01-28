using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsHandler : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 5;
    [SerializeField] private AmmoType ammoType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            FindObjectOfType<Ammo>().IncreaaseCurrentAmmo(ammoType,ammoAmount);
            Debug.Log("Triggered by player");
            Destroy(gameObject);
        }
    }
}
