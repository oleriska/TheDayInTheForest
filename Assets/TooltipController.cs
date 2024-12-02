using UnityEngine;

public class TooltipController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI Key;
    [SerializeField]
    private TMPro.TextMeshProUGUI DescriptionText;

    public void SetToolTip(string key, string text)
    {
        Key.text = key;
        DescriptionText.text = text;   
    }
}
