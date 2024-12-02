using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DisplayDialog : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI Name;
    [SerializeField]
    private TMPro.TextMeshProUGUI Text;
    [SerializeField]
    private Image Icon;

    public void SetDialog(Dialog dialog)
    {
        Name.text = dialog.Name;
        Text.text = dialog.Text;
        Icon.sprite = dialog.Image;
    }
}
