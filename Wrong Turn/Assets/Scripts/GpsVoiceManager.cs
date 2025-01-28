using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GpsVoiceManager : MonoBehaviour
{
   [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip triggerSound;
    
    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (triggerSound != null)
        {
            audioSource.PlayOneShot(triggerSound);
        }
    }
}