using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemChangePanel : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI ItemText;
    [SerializeField]
    private float FadeOutDelay;

    private Image Panel;

    void Awake()
    {
        Panel = GetComponent<Image>();
    }

    public void SetPanelInfo(string ItemName, bool added)
    {
        if(added)
        {
            ItemText.text = $"+1 {ItemName}";
            Panel.color = new Color(0.05f, 1f, 0.07f, 0.7f); // green
        }
        else
        {
            ItemText.text = $"-1 {ItemName}";
            Panel.color = new Color(0.5f, 0.07f, 0.07f, 0.7f); // red
        }
        StartCoroutine(WaitAndFadeOut());
    }

    IEnumerator WaitAndFadeOut()
    {
        yield return new WaitForSeconds(FadeOutDelay);
        yield return StartCoroutine(FadeOut());
        Destroy(gameObject);
    }

    private IEnumerator FadeOut()
    {
        Color color = Panel.color;
        Color textColor = ItemText.color;
        float startAlpha = color.a;
        float startAlphaText = textColor.a;
        float timer = 0;

        while (timer < 2)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0, timer / 2);
            textColor.a = Mathf.Lerp(startAlphaText, 0, timer / 2);
            Panel.color = color;
            ItemText.color = textColor;
            yield return null;
        }

        color.a = 0;
        textColor.a = 0;
        Panel.color = color;
        ItemText.color = textColor;
    }
}
