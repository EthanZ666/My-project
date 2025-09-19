using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Loads the main game scene
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
