using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // replace with your actual game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting..."); // visible in editor only
    }
}
