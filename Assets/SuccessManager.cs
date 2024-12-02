using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessManager : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Exit()
    {
        Application.Quit(); 
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

}
