using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string[] dialogueLines;  
    public DialogueManager dialogueManager;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(dialogueLines);
        }
    }
}
