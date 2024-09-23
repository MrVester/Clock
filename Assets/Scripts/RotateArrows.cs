using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrows : MonoBehaviour
{
    private float screenWidth;
    private Vector3 startMousePos;
    private Quaternion StartRotation;
    private Camera _mainCam;
    private RaycastHit2D rayHit;
    public bool canChangeTime = false;
    /*    [SerializeField] private Transform HArrow;
        [SerializeField] private Transform MArrow;*/
    private void Start()
    {
        _mainCam = Camera.main;
        screenWidth = Screen.width;
    }

    void Update()
    {
        if (canChangeTime)
        if (Input.GetMouseButtonDown(0))
        {

            rayHit = Physics2D.GetRayIntersection(_mainCam.ScreenPointToRay(Input.mousePosition));
            if (rayHit)
            {
                startMousePos = Input.mousePosition;
                StartRotation = rayHit.collider.gameObject.transform.rotation;
            }
            
        }
        else if(Input.GetMouseButton(0) && rayHit)
        {
          
            float distance = (Input.mousePosition - startMousePos).x;
            rayHit.collider.gameObject.transform.rotation = StartRotation * Quaternion.Euler(Vector3.forward * (distance / screenWidth) * -360);
        }
        
    }
}
