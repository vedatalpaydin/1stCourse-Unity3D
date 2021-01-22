using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float speed = 20f;
    [Tooltip("In m")][SerializeField] float xRange = 5f;
    [Tooltip("In m")][SerializeField] float yRange = 3f;
    
   [SerializeField] float positionPitchFactor = -5f;
   [SerializeField] float controlPitchFactor = -20f;
   [SerializeField] float positionYawFactor = 5f;
   [SerializeField] float controlRollFactor = -20f;

    float yThrow, xThrow;
   void Start()
    {
        
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
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

        float yOffset = yThrow * speed * Time.deltaTime;
        float xOffset = xThrow * speed * Time.deltaTime;
        
        float rawYPos = transform.localPosition.y + yOffset; 
        float rawXPos = transform.localPosition.x + xOffset;

        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange); 
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
