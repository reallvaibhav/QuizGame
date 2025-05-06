using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    // Method to load the quiz scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Mcq"); // Replace "Quiz" with the name of your quiz scene
    }

    // Method to quit the application
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // This will show in the editor console
        Application.Quit(); // This will quit the application when built
    }
}