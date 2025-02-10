using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinaleTrigger : MonoBehaviour
{
    public Camera mainCamera;
    public Camera finaleCamera;
    public Image blackScreen;
    public float fadeDuration = 2f;
    public AudioSource staticSound;
    public AudioClip finalGPSLine;
    public AudioSource gpsAudioSource;
    public float waitBeforeGpsLine = 3f;

   private void Start()
   {
        if (finaleCamera != null)
                     finaleCamera.enabled = false;

        if (mainCamera != null)
        mainCamera.enabled = true;
   }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the final trigger");
            StartCoroutine(PlayFinaleSequence());
        }
    }

   private IEnumerator PlayFinaleSequence()
   {
        Debug.Log("Starting finale sequence");

        if (staticSound) staticSound.Play();
        Debug.Log("starting fade to black....");
        yield return StartCoroutine(FadeToBlack());
        Debug.Log("fade to black complete");

        Debug.Log("switching cameras...");

        if (mainCamera.TryGetComponent(out CameraLook cameraLook))
        {
            Debug.Log("Disabling camera look script");
            cameraLook.enabled = false;
        }

        if (mainCamera && finaleCamera)
        {
            mainCamera.gameObject.SetActive(false);
            finaleCamera.enabled = true;
            Debug.Log("Switched to finale camera");
        }

        yield return StartCoroutine(FadeFromBlack());

        yield return new WaitForSeconds(waitBeforeGpsLine);

        if (gpsAudioSource != null && finalGPSLine != null)
        {
            gpsAudioSource.PlayOneShot(finalGPSLine);
        }

        yield return new WaitForSeconds(finalGPSLine.length);

        SceneManager.LoadScene("Menu");
   }

   private IEnumerator FadeToBlack()
   {
        Debug.Log("fading to black...");

        float duration = 1.5f;
        float elapsedTime = 0f;

        Color startColor = blackScreen.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            blackScreen.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        blackScreen.color = targetColor;
   }

   private IEnumerator FadeFromBlack()
   {
        float elapsedTime = 0f;
        Color color = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            blackScreen.color = color;
            yield return null; 

        }
   }
}
   
