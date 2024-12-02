using UnityEngine;
using UnityEngine.UI;


public class DisplayItem : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI ItemText;
    [SerializeField]
    private Image Icon;
    private HandsController _handsController;

    private Item _item;

    private void Start()
    {
        _handsController = GameObject.FindGameObjectWithTag("Player").GetComponent<HandsController>();
    }
    public void SetItem(Item item)
    {
        _item = item;
        ItemText.text = item.Name;
        Icon.sprite = item.Image;
    }

    public void HandleEquip()
    {
        if (!_item.Equipable)
            return;

        _handsController.SetItem(_item);
    }
}
