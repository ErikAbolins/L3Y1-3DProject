using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesLookAtPlayer : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5f;
    public Transform leftEye; 
    public Transform rightEye;
    public Transform plane;

    public float lookAtDuration = 3f;
    private float gazeTimer = 0f;

    private Renderer leftEyeRenderer;
    private Renderer rightEyeRenderer;
    private Renderer planeRenderer;

    void Start()
    {
        
        leftEye = transform.Find("LeftEye"); 
        rightEye = transform.Find("RightEye"); 
        plane = transform.Find("Plane");

        if (leftEye != null) leftEyeRenderer = leftEye.GetComponent<Renderer>();
        if (rightEye != null) rightEyeRenderer = rightEye.GetComponent<Renderer>();
        if (plane != null) planeRenderer = plane.GetComponent<Renderer>();
    }

    void Update()
    {
        if (leftEye != null && rightEye != null && plane != null)
        {
            RotateEyeTowardsPlayer(leftEye);
            RotateEyeTowardsPlayer(rightEye);

            FollowEyesWithPlane();

            CheckIfPlayerIsLooking();
        }
    }

    void RotateEyeTowardsPlayer(Transform eye)
    {
        Vector3 directionToPlayer = player.position - eye.position;
        directionToPlayer.y = 0; 

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        eye.localRotation = Quaternion.Slerp(eye.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void FollowEyesWithPlane()
    {
        Vector3 averageEyePosition = (leftEye.position + rightEye.position) / 2;

        plane.position = averageEyePosition;
    }

    void CheckIfPlayerIsLooking()
    {
        Vector3 directionToEyes = (leftEye.position + rightEye.position) / 2 - player.position;
        directionToEyes.y = 0;

        float dotProduct = Vector3.Dot(player.forward, directionToEyes.normalized);

        //Debug.Log("Dot Product: " + dotProduct);

        if (dotProduct > 0.6f)
        {
            gazeTimer += Time.deltaTime;
            Debug.Log("Gaze Timer: " + gazeTimer); 

            if (gazeTimer >= lookAtDuration)
            {
                HideEyesAndPlane();
            }
        }
        else
        {
            gazeTimer = 0f;
        }
    }

    void HideEyesAndPlane()
    {
        if (leftEyeRenderer != null) 
        {
            leftEyeRenderer.enabled = false;
            Debug.Log("Left Eye Hidden");
        }
        if (rightEyeRenderer != null) 
        {
            rightEyeRenderer.enabled = false;
            Debug.Log("Right Eye Hidden");
        }
        if (planeRenderer != null) 
        {
            planeRenderer.enabled = false;
            Debug.Log("Plane Hidden");
        }
    }
}