using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float sensitivity = 100f;
    public float maxLookAngle = 45f;

    private float currentYRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        currentYRotation = Mathf.Clamp(currentYRotation + mouseX, -maxLookAngle, maxLookAngle);
        
        transform.localRotation = Quaternion.Euler(0f, currentYRotation, 0f);
    }
}
