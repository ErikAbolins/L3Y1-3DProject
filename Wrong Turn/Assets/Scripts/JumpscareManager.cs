using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareManager : MonoBehaviour
{
   public GameObject Jumpscare;
   public AudioSource knockSound;
   public AudioClip scareSound;
   public float scareDuration = 2f;

   public Transform playerCamera;
   public float turnAngle = 45f;
   public float turnSpeed = 2f;

   private bool scareTriggered = false;

   void OnTriggerEnter(Collider other)
   {
    if (other.CompareTag("Player") && !scareTriggered)
    {
        scareTriggered = true;
        StartCoroutine(TriggerJumpscare());
        StartCoroutine(TurnCameraLeft());
    }
   }

   IEnumerator TriggerJumpscare()
   {
    knockSound.Play();

    yield return new WaitForSeconds(knockSound.clip.length);
    
    Jumpscare.SetActive(true);

    yield return new WaitForSeconds(scareDuration);

    AudioSource.PlayClipAtPoint(scareSound, Jumpscare.transform.position);
    
    Jumpscare.SetActive(false);
   }

   IEnumerator TurnCameraLeft()
   {
        float targetAngle = playerCamera.localEulerAngles.y - turnAngle;
        while (Mathf.Abs(Mathf.DeltaAngle(playerCamera.localEulerAngles.y, targetAngle)) > 0.1f)
        {
            float newAngle = Mathf.MoveTowardsAngle(playerCamera.localEulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            playerCamera.localEulerAngles = new Vector3(playerCamera.localEulerAngles.x, newAngle, playerCamera.localEulerAngles.z);
            yield return null;
        }
   }
}
