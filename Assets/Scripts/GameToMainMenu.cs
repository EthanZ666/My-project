using UnityEngine;
using UnityEngine.SceneManagement;

public class GameToMainMenu : MonoBehaviour
{
    // Loads the main menu scene
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
