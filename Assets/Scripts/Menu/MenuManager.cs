using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject levelSelection;
    [SerializeField]
    private GameObject settings;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene2(string sceneName, string param)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void EnableMain()
    {
        settings.SetActive(false);
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void EnableLevelSelection()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }
    public void EnableSettings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }    
    public void HandleExit()
    {
        Application.Quit();
    }
}
