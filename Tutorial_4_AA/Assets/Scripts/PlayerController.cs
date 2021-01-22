using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")][SerializeField] float xRange = 5f;
    [Tooltip("In m")][SerializeField] float yRange = 3f;
    [SerializeField] GameObject[] guns;
    
    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -5f;
   [SerializeField] float controlPitchFactor = -20f;
   
   [Header("Control-throw Based")]
   [SerializeField] float positionYawFactor = 5f;
   [SerializeField] float controlRollFactor = -20f;
   
    float yThrow, xThrow;
     bool isControlEnabled = true;
     void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }
    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x* positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation=Quaternion.Euler(pitch,yaw,roll);
    }

    void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal"); 
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        
        float rawYPos = transform.localPosition.y + yOffset; 
        float rawXPos = transform.localPosition.x + xOffset;

        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange); 
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    } 
    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;

        }
    }
}
