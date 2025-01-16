using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string[] dialogueLines;  // The dialogue lines for this trigger zone
    public DialogueManager dialogueManager;  // Reference to the DialogueManager script

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that triggered the collider is the player
        if (other.CompareTag("Player"))
        {
            // Start the dialogue when the player enters the trigger zone
            dialogueManager.StartDialogue(dialogueLines);
        }
    }
}
