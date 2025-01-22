using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonJumpscare : MonoBehaviour
{
  public GameObject jumpscareImage; 
    public AudioClip jumpscareSound; 
    public float jumpscareDuration = 2f; 

    private AudioSource audioSource;

    void Start()
    {
        if (jumpscareImage != null)
        {
            jumpscareImage.SetActive(false);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnStartButtonPressed()
    {
        StartCoroutine(TriggerJumpscare());
    }

    private IEnumerator TriggerJumpscare()
    {
      
        if (jumpscareImage != null)
        {
            jumpscareImage.SetActive(true);
        }

       
        if (jumpscareSound != null)
        {
            audioSource.PlayOneShot(jumpscareSound);
        }

        yield return new WaitForSeconds(jumpscareDuration);

        if (jumpscareImage != null)
        {
            jumpscareImage.SetActive(false);
        }

        SceneManager.LoadScene("Main Game");
    }
}