using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystemSc : MonoBehaviour
{
    [SerializeField] private float lightDecay = .1f;
    [SerializeField] private float angleDecay = 1f;
    [SerializeField] private float minimunAngle = 40f;

    private Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = restoreAngle;
    }

    public void RestoreLightIntensity(float intensityAmount)
    {
        myLight.intensity += intensityAmount; 
    }

    private void DecreaseLightIntensity()
    {
        myLight.intensity -= lightDecay * Time.deltaTime;
    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle <= minimunAngle)
        {
            return;
        }
        else
        {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }
}
