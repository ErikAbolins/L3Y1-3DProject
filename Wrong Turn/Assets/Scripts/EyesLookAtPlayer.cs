using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesLookAtPlayer : MonoBehaviour
{
    public Transform player; // The player's transform to follow.
    public float rotationSpeed = 5f; // Speed of rotation.
    public Transform leftEye; // Reference to the left eye.
    public Transform rightEye; // Reference to the right eye.
    public Transform plane; // Reference to the blacked-out plane.

    public float lookAtDuration = 3f; // How long the player needs to look at the eyes.
    private float gazeTimer = 0f; // Timer for how long the player has been looking.

    private Renderer leftEyeRenderer;
    private Renderer rightEyeRenderer;
    private Renderer planeRenderer;

    void Start()
    {
        // Find the left and right eyes based on their names or assign directly if you already have references.
        leftEye = transform.Find("LeftEye"); // Replace "LeftEye" with the exact name if different.
        rightEye = transform.Find("RightEye"); // Replace "RightEye" with the exact name if different.
        plane = transform.Find("Plane"); // Find the plane (adjust name as needed)

        // Cache the Renderers for the eyes and plane
        if (leftEye != null) leftEyeRenderer = leftEye.GetComponent<Renderer>();
        if (rightEye != null) rightEyeRenderer = rightEye.GetComponent<Renderer>();
        if (plane != null) planeRenderer = plane.GetComponent<Renderer>();
    }

    void Update()
    {
        if (leftEye != null && rightEye != null && plane != null)
        {
            // Rotate both eyes to face the player.
            RotateEyeTowardsPlayer(leftEye);
            RotateEyeTowardsPlayer(rightEye);

            // Move the plane to follow the eyes.
            FollowEyesWithPlane();

            // Check if the player is looking at the eyes.
            CheckIfPlayerIsLooking();
        }
    }

    void RotateEyeTowardsPlayer(Transform eye)
    {
        // Calculate the direction towards the player.
        Vector3 directionToPlayer = player.position - eye.position;
        directionToPlayer.y = 0; // Keep the rotation only on the horizontal plane (X-Z axis).

        // Calculate the desired rotation (look rotation).
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Smoothly rotate the eye towards the player.
        eye.localRotation = Quaternion.Slerp(eye.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void FollowEyesWithPlane()
    {
        // Calculate the average position between the left and right eyes.
        Vector3 averageEyePosition = (leftEye.position + rightEye.position) / 2;

        // Set the plane's position to be the same as the average eye position.
        plane.position = averageEyePosition;
    }

    void CheckIfPlayerIsLooking()
    {
        // Direction from the eyes to the player.
        Vector3 directionToEyes = (leftEye.position + rightEye.position) / 2 - player.position;
        directionToEyes.y = 0; // Make sure we only check horizontal direction.

        // Dot product to check if the player is looking at the eyes
        float dotProduct = Vector3.Dot(player.forward, directionToEyes.normalized);

        // Debug to check the dot product
        Debug.Log("Dot Product: " + dotProduct);

        // If the dot product is above a certain threshold, the player is looking at the eyes
        if (dotProduct > 0.6f) // Lowered the threshold to 0.6 for testing
        {
            // Increment the gaze timer if the player is looking at the eyes
            gazeTimer += Time.deltaTime;
            Debug.Log("Gaze Timer: " + gazeTimer); // Debug the timer

            // If the player has looked at them for too long, hide the eyes and plane
            if (gazeTimer >= lookAtDuration)
            {
                HideEyesAndPlane();
            }
        }
        else
        {
            // Reset the timer if the player looks away
            gazeTimer = 0f;
        }
    }

    void HideEyesAndPlane()
    {
        // Hide the eyes and plane by disabling the renderers
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