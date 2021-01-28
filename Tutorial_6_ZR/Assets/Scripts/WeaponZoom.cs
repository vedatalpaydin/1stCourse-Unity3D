using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private bool isZoom = false;
    private RigidbodyFirstPersonController _firstPersonController;

     void OnDisable()
    {
        isZoom = false;
    }
    void Start()
    {
        _firstPersonController = GetComponent<RigidbodyFirstPersonController>();
    }
    void Update()
    {
        ZoomIn();
    }

    private void ZoomIn()
    {
        if (Input.GetButton("Fire2"))
        {
            isZoom = true;
            _camera.fieldOfView = 20f;
            
            _firstPersonController.mouseLook.XSensitivity =.5f;
            _firstPersonController.mouseLook.YSensitivity = .5f;
        }
        else
        {
            isZoom = false;
            _camera.fieldOfView = 60f;
            _firstPersonController.mouseLook.XSensitivity =2f;
            _firstPersonController.mouseLook.YSensitivity = 2f;
        }
    }
}
