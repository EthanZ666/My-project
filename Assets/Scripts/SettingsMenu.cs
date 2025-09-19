using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    void Start()
    {
        // Load saved settings or set defaults
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        bool savedFullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;

        // Apply them
        AudioListener.volume = savedVolume;
        Screen.fullScreen = savedFullscreen;

        // Update UI
        if (volumeSlider != null) volumeSlider.value = savedVolume;
        if (fullscreenToggle != null) fullscreenToggle.isOn = savedFullscreen;
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
    }
}
