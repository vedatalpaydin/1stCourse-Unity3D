using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] private float restoreAngle = 90f;
    [SerializeField] private float addIntensity = 1f;


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player")
        {
            col.GetComponentInChildren<LightSystemSc>().RestoreLightAngle(restoreAngle);
            col.GetComponentInChildren<LightSystemSc>().RestoreLightIntensity(addIntensity);
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
