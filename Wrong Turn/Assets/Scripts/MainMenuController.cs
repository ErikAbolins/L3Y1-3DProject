using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // Load the main game scene (make sure the scene is added to the build settings)
        SceneManager.LoadScene("Main Game");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
