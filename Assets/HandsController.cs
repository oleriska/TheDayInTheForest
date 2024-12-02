using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    public static HandsController Instance;
    [SerializeField]
    private Transform HoldPosition;
    public Item EquippedItem;

    private GameObject HoldingInstance;
    private Item Hands;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        Hands = new Item("Hands", "");
    }

    public void SetItem(Item item)
    {
        if (item.Name == EquippedItem?.Name)
        {
            Destroy(HoldingInstance);
            EquippedItem = Hands;
            return;
        }

        Destroy(HoldingInstance);

        var holdItem = Resources.Load<GameObject>($"Prefabs/{item.Name}");

        HoldingInstance = Instantiate(holdItem, HoldPosition);
        EquippedItem = item;
    }

    public void Disarm()
    {
        Destroy(HoldingInstance);
        EquippedItem = Hands;
        return;
    }
}
