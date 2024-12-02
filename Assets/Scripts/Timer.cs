using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI TimerText;
    [SerializeField]
    private float RemainingTime = 300;

    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private float fadeDuration = 2f;

    void Update()
    {
        if (RemainingTime > 0)
        {
            RemainingTime -= Time.deltaTime;
        }
        else if (RemainingTime < 0)
        {
            RemainingTime = 0;
            StartCoroutine(LoadFailureScene());
        }
        int minutes = Mathf.FloorToInt(RemainingTime / 60);
        int seconds = Mathf.FloorToInt(RemainingTime % 60);
        TimerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    IEnumerator LoadFailureScene()
    {
        yield return StartCoroutine(FadeToBlack());
        SceneManager.LoadScene("Fail");
    }

    private IEnumerator FadeToBlack()
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
