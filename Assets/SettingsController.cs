using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] resolutions;

    void Start()
    {
        LoadResolutions();
        LoadSettings();
    }

    private void LoadResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // Create a list of resolution options as strings
        var options = new System.Collections.Generic.List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Check if this is the current screen resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void ApplySettings()
    {
        // Apply Fullscreen setting
        bool isFullscreen = fullscreenToggle.isOn;
        Screen.fullScreen = isFullscreen;

        // Apply resolution setting
        int selectedResolutionIndex = resolutionDropdown.value;
        Resolution selectedResolution = resolutions[selectedResolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullscreen);

        // Save settings
        SaveSettings();
    }

    public void LoadSettings()
    {
        // Load fullscreen setting
        bool isFullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1; // Default to fullscreen
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;

        // Load resolution setting
        int resolutionIndex = PlayerPrefs.GetInt("resolutionIndex", 0); // Default to the first resolution
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            resolutionDropdown.value = resolutionIndex;
            resolutionDropdown.RefreshShownValue();

            Resolution savedResolution = resolutions[resolutionIndex];
            Screen.SetResolution(savedResolution.width, savedResolution.height, isFullscreen);
        }
    }

    private void SaveSettings()
    {
        // Save fullscreen setting
        PlayerPrefs.SetInt("fullscreen", fullscreenToggle.isOn ? 1 : 0);

        // Save resolution index
        PlayerPrefs.SetInt("resolutionIndex", resolutionDropdown.value);

        PlayerPrefs.Save();
    }
}
