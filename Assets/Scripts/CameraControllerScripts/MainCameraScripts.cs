using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScripts : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target; 
    public float gap = 20f; 
    public float heightOffset = 10f; 
    public float sensitivity = 5f; 
    public float MinVerAngle = -14f; 
    public float MaxVerAngle = 45f; 
    public float smoothSpeed = 15f; 

    private float rotX; 
    private float rotY; 

    private void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
        Vector3 angles = transform.eulerAngles;
        rotX = angles.x;
        rotY = angles.y;
    }

    private void LateUpdate()
    {
       
        rotX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotY += Input.GetAxis("Mouse X") * sensitivity; 

        
        rotX = Mathf.Clamp(rotX, MinVerAngle, MaxVerAngle);

        
        Quaternion targetRotation = Quaternion.Euler(rotX, rotY, 0);

        
        Vector3 desiredPosition = target.position
                                  + targetRotation * new Vector3(0, heightOffset, -gap);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

       
        transform.LookAt(target.position + Vector3.up * heightOffset);
    }

    public Quaternion flatRotation => Quaternion.Euler(0, rotY,0);
}
