using UnityEngine;

public class ItemChangeSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject Content;
    [SerializeField]
    private GameObject PanelPrefab;
    
    void Start()
    {
        InventoryManager.Instance.OnItemAdded += SpawnAddedItemPanel;
        InventoryManager.Instance.OnItemRemoved += SpawnRemovedItemPanel;
    }

    private void SpawnRemovedItemPanel(Item item) => SpawnPanel(item, false);

    private void SpawnAddedItemPanel(Item item) => SpawnPanel(item, true);

    private void SpawnPanel(Item item, bool added)
    {
        GameObject newPanel = Instantiate(PanelPrefab);
        newPanel.transform.SetParent(Content.transform, false);
        newPanel.GetComponent<ItemChangePanel>().SetPanelInfo(item.Name, added);
    }
}
