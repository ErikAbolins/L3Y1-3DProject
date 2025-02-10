using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSequence : MonoBehaviour
{
    public CarController carController;
    public float chaseSpeed = 20f;
    public float normalSpeed;
    public AudioSource chaseMusic;
    public AudioSource RunSound;
    public float fadeOutDuration = 2f;

    private void Start()
    {
        normalSpeed = carController.speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            carController.speed = chaseSpeed;
            chaseMusic.Play(); 
            RunSound.Play();
        }
    }

    public void ResetSpeed()
    {
        carController.speed = normalSpeed;
        StartCoroutine(FadeOutSound(chaseMusic));
        StartCoroutine(FadeOutSound(RunSound));
    }

    public IEnumerator FadeOutSound(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeOutDuration);
            yield return null;
        }

        audioSource.Stop();
    }
    
}
