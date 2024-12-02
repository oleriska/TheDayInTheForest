using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCraft : MonoBehaviour
{
    [SerializeField]
    private string Name;
    [SerializeField]
    private string Description;
    [SerializeField]
    private bool Equipable;
    [SerializeField]
    private Sprite ItemImage;
    [SerializeField]
    private List<string> RequiredItems = new List<string>();
    [SerializeField]
    private TMPro.TextMeshProUGUI ItemText;
    [SerializeField]
    private TMPro.TextMeshProUGUI DescriptionText;
    [SerializeField]
    private Image Icon;

    private Image _buttonColors;
    private bool _craftAvailable;

    private void CheckAvailability()
    {
        foreach (var item in RequiredItems)
        {
            bool found = InventoryManager.Instance.PlayerItems.Any(i => i.Name == item);

            if (found == false)
            {
                var current = _buttonColors.color;
                _buttonColors.color = new Color(0.5f, 0.07f, 0.07f, 0.7f); // red
                _craftAvailable = false;
                return;
            }
        }
        _buttonColors.color = new Color(0.05f, 0.5f, 0.07f, 0.7f); // green
        _craftAvailable = true;
    }
    private void Start()
    {
        _buttonColors = GetComponent<Image>();
        CheckAvailability();
        InventoryManager.Instance.onItemPickup += CheckAvailability;
        ItemText.text = Name;
        DescriptionText.text = Description;
    }

    public void Craft()
    {
        if (_craftAvailable == false)
            return;

        foreach (var reqItem in RequiredItems)
        {
            var item = InventoryManager.Instance.PlayerItems.FirstOrDefault(i => i.Name == reqItem);

            if (item == null)
                continue;

            InventoryManager.Instance.RemoveItem(item);
        }
        InventoryManager.Instance.AddItem(new Item(Name, DescriptionText.text, Equipable));
        HandsController.Instance.Disarm();
        CheckAvailability();
    }
}
