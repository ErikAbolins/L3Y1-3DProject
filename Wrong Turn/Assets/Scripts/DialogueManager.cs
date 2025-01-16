using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject chatBox;  // Reference to the UI chatbox
    public TextMeshProUGUI dialogueText;  // Reference to the dialogue text field
    private Queue<string> sentences = new Queue<string>();

    private void Start()
    {
        chatBox.SetActive(false);  // Start with the chatbox hidden
    }

    // Start the dialogue with a list of sentences
    public void StartDialogue(string[] dialogue)
    {
        chatBox.SetActive(true);  // Show the chatbox
        sentences.Clear();  // Clear any previous sentences

        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);  // Add each sentence to the queue
        }

        DisplayNextSentence();  // Display the first sentence immediately
    }

    // Display the next sentence in the queue
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            StartCoroutine(EndDialogueAfterDelay());  // If no sentences left, wait for a few seconds before ending the dialogue
            return;
        }

        string sentence = sentences.Dequeue();  // Get the next sentence
        dialogueText.text = sentence;  // Display the sentence on the UI

        // If it's the last sentence, wait a bit before hiding the chatbox
        if (sentences.Count == 0)
        {
            StartCoroutine(EndDialogueAfterDelay());  // Automatically hide chatbox after the last sentence
        }
    }

    // Coroutine to wait for a few seconds before hiding the chatbox
    private IEnumerator EndDialogueAfterDelay()
    {
        yield return new WaitForSeconds(2f);  // Wait for 2 seconds (adjust time as needed)
        EndDialogue();  // End the dialogue and hide the chatbox
    }

    // End the dialogue and hide the chatbox
    public void EndDialogue()
    {
        chatBox.SetActive(false);  // Hide the chatbox
    }
}
