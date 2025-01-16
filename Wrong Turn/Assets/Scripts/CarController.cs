using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;              // Max speed
    public float acceleration = 2f;        // Acceleration rate
    public float deceleration = 3f;        // Deceleration rate
    public float steeringAngle = 30f;      // Steering angle
    public float brakeForce = 5f;          // Brake force

    public float currentSpeed = 0f;        // Current speed of the car
    private float horizontalInput;         // Steering input
    private float verticalInput;           // Forward/backward input

    public AudioSource engineIdleSound;    // Reference to the idle sound
    public AudioSource engineDrivingSound; // Reference to the driving sound

    private void Update()
    {
        // Get player input
        horizontalInput = Input.GetAxis("Horizontal"); // Left/Right movement
        verticalInput = Input.GetAxis("Vertical");     // Forward/Backward movement

        // Accelerate or decelerate the car based on input
        if (verticalInput > 0)
        {
            currentSpeed += acceleration * verticalInput * Time.deltaTime;
        }
        else if (verticalInput < 0)
        {
            currentSpeed -= brakeForce * -verticalInput * Time.deltaTime; // Brake when moving backward
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime); // Slow down when no input
        }

        // Clamp the current speed between a negative speed and max speed
        currentSpeed = Mathf.Clamp(currentSpeed, -speed / 2f, speed);

        // Steering the car
        float steer = horizontalInput * steeringAngle * Time.deltaTime;
        transform.Rotate(0f, steer, 0f);

        // Move the car forward or backward
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Call the method to update the engine sounds based on speed
        UpdateEngineSounds();
    }

    private void UpdateEngineSounds()
    {
        // If car is moving forward, play the driving sound and stop idle sound
        if (currentSpeed > 0)
        {
            if (!engineDrivingSound.isPlaying)
            {
                engineDrivingSound.Play();
            }

            if (engineIdleSound.isPlaying)
            {
                engineIdleSound.Stop();
            }

            // Adjust pitch of driving sound based on current speed
            engineDrivingSound.pitch = Mathf.Lerp(1f, 2f, currentSpeed / speed);
        }
        else
        {
            if (engineDrivingSound.isPlaying)
            {
                engineDrivingSound.Stop();
            }

            if (!engineIdleSound.isPlaying)
            {
                engineIdleSound.Play();
            }
        }
    }
}
