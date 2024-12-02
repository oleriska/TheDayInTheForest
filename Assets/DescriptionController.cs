using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DescriptionController : MonoBehaviour
{
    [SerializeField]
    private string LevelName;
    [SerializeField]
    private string Description;
    [SerializeField]
    private TMPro.TextMeshProUGUI LevelNameText;
    [SerializeField]
    private TMPro.TextMeshProUGUI DescriptionText;

    public void SetDescription(string info)
    {
        List<string> infoArr = info.Split(':').ToList();
        LevelName = infoArr[0];
        Description = infoArr[1];
        LevelNameText.text = infoArr[0];
        DescriptionText.text = infoArr[1];
    }

    private void Start()
    {
        LevelNameText.text = LevelName;
        DescriptionText.text = Description;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelName);
    }
}
