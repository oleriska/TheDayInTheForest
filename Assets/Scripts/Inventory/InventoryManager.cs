using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField]
    public List<Item> PlayerItems = new List<Item>();
    [SerializeField]
    private GameObject Inventory;
    [SerializeField]
    private GameObject InventoryContent;
    [SerializeField]
    private GameObject CraftingContent;
    [SerializeField]
    private GameObject ItemPrefab;
    [SerializeField]
    private GameObject Compass;

    private PlayerInput _inputAsset;
    private InputAction _openInventory;
    private InputAction _toggleCompass;
    private StarterAssetsInputs playerInputs;

    public event Action<Item> OnItemAdded;
    public event Action<Item> OnItemRemoved;

    public void OnItemAddedTrigger(Item item)
    {
        OnItemAdded?.Invoke(item);
    }
    public void OnItemRemovedTrigger(Item item)
    {
        OnItemRemoved?.Invoke(item);
    }

    private void Awake()
    {
        _inputAsset = GetComponent<PlayerInput>();
        playerInputs = GetComponent<StarterAssetsInputs>();

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _openInventory = _inputAsset.actions.FindAction("OpenInventory");
        _toggleCompass = _inputAsset.actions.FindAction("ToggleCompass");
        _openInventory.performed += ToggleInventory;
        _toggleCompass.performed += ToggleCompass;
    }

    private void ToggleCompass(InputAction.CallbackContext context)
    {
        if (Compass.activeInHierarchy == true)
        {
            Compass.SetActive(false);
            return;
        }
        Compass.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var pickup = other.gameObject.GetComponent<Pickup>();

        if (pickup != null)
        {
            if (!PlayerItems.Contains(pickup.Item))
            {
                AddItem(pickup.Item);
            }
            Destroy(pickup.gameObject);
        }
    }

    public void RemoveItem(Item item)
    {
        PlayerItems.Remove(item);
        UpdateInventoryUI();
        OnItemRemovedTrigger(item);
    }
    public void AddItem(Item item)
    {
        PlayerItems.Add(item);
        UpdateInventoryUI();
        OnItemAddedTrigger(item);
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        if(Inventory.activeInHierarchy == true)
        {
            Inventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            playerInputs.cursorInputForLook = true;
            return;
        }
        Inventory.SetActive(true);
        UpdateInventoryUI();
        Cursor.lockState = CursorLockMode.None;
        playerInputs.cursorInputForLook = false;
    }

    private void UpdateInventoryUI()
    {
        foreach (Transform child in InventoryContent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in PlayerItems)
        {
            var itemGO = Instantiate(ItemPrefab);
            itemGO.transform.SetParent(InventoryContent.transform, false);
            itemGO.GetComponent<DisplayItem>().SetItem(item);
        }
    }
    public void EnableInventoryContent()
    {
        InventoryContent.SetActive(true);
        CraftingContent.SetActive(false);
    }
    public void EnableCraftingContent()
    {
        InventoryContent.SetActive(false);
        CraftingContent.SetActive(true);
    }
}
