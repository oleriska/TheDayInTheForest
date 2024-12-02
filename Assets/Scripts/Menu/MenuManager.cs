using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject levelSelection;

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
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void EnableLevelSelection()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }
    public void HandleExit()
    {
        Application.Quit();
    }
}
