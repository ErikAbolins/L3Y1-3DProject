using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarSoundController : MonoBehaviour
{
    public AudioSource idleAudio; // Audio source for the idle sound
    public AudioSource drivingAudio; // Audio source for the driving sound
    public Rigidbody carRigidbody; // Reference to the car's Rigidbody
    public float minSpeedForDriving = 1.0f; // Speed threshold to switch to driving sound
    public float minPitch = 0.8f;  // Minimum pitch of the engine sound
    public float maxPitch = 2.0f;  // Maximum pitch of the engine sound
    public float speedToPitchFactor = 0.05f; // Factor to scale speed to pitch

    private void Start()
    {
        // Ensure both audio sources are initialized
        if (idleAudio == null)
        {
            idleAudio = GetComponent<AudioSource>();
        }

        if (drivingAudio == null)
        {
            drivingAudio = gameObject.AddComponent<AudioSource>(); // Add a second audio source for driving sound
        }

        if (carRigidbody == null)
        {
            carRigidbody = GetComponent<Rigidbody>();
        }

        // Set both audio sources to loop
        idleAudio.loop = true;
        drivingAudio.loop = true;

        idleAudio.Play(); // Start playing idle sound
        drivingAudio.Stop(); // Ensure driving sound is stopped initially
    }

    private void Update()
    {
        // Get car speed
        float speed = carRigidbody.velocity.magnitude;

        // Debug log to check the current speed
        Debug.Log("Car speed: " + speed);

        // Adjust pitch based on speed for idle and driving sounds
        idleAudio.pitch = Mathf.Clamp(minPitch + speed * speedToPitchFactor, minPitch, maxPitch);
        drivingAudio.pitch = Mathf.Clamp(minPitch + speed * speedToPitchFactor, minPitch, maxPitch);

        // Switch between idle and driving sounds based on speed
        if (speed > minSpeedForDriving)
        {
            if (!drivingAudio.isPlaying)
            {
                Debug.Log("Switching to driving sound");
                drivingAudio.Play();  // Play the driving sound when car is moving
            }
            if (idleAudio.isPlaying)
            {
                idleAudio.Stop(); // Stop the idle sound
            }
        }
        else
        {
            if (!idleAudio.isPlaying)
            {
                Debug.Log("Switching to idle sound");
                idleAudio.Play(); // Play idle sound when the car is stationary or slow
            }
            if (drivingAudio.isPlaying)
            {
                drivingAudio.Stop(); // Stop the driving sound
            }
        }
    }
}
