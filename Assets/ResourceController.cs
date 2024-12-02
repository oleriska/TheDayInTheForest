using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceController : MonoBehaviour
{
    [SerializeField]
    private string TooltipText;
    [SerializeField]
    private GameObject TooltipPrefab;
    [SerializeField]
    private GameObject SSTooltipPrefab;
    [SerializeField]
    private float TooltipHeight = 0.4f;
    [SerializeField]
    private float TooltipZOffset = 0f;
    [SerializeField]
    private Item FarmItem;
    [SerializeField]
    private bool OneTimeFarm;
    [SerializeField]
    private string RequiredItem;
    [SerializeField]
    private bool DiscardRequiredItem;
    [SerializeField]
    private GameObject ActivateObject;
    [SerializeField]
    private string NewObjective;
    [SerializeField]
    private int FarmCountToActivate;
    [SerializeField]
    private bool SpawnTooltipOnSSCanvas;
    [SerializeField]
    private bool IsTree;

    private Transform _WScanvas;
    private Transform _SScanvas;
    private Transform _playerCamera;
    private InputAction _farm;
    private PlayerInput _playerInput;
    private int _farmCount;
    

    private GameObject tooltipInstance;

    private void Start()
    {
        _WScanvas = GameObject.FindGameObjectWithTag("WSCanvas").GetComponent<Transform>();
        _SScanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
        _playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        _farm = _playerInput.actions.FindAction("Farm");
        _farm.performed += Farm;

        if(_playerInput == null)
        {
            Debug.Log("nulll");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != _playerInput.gameObject)
            return;

        if(tooltipInstance != null)
            return;

        if (!SpawnTooltipOnSSCanvas)
        {
            Transform playerTransform = other.transform;
            Vector3 directionToPlayer = (playerTransform.position - gameObject.transform.position).normalized;
            Vector3 spawnOffset = new Vector3(0, TooltipHeight, 0);
            float distanceCloser = TooltipZOffset;
            Vector3 closerToPlayerOffset = directionToPlayer * distanceCloser;
            Vector3 spawnPosition = gameObject.transform.position + spawnOffset + closerToPlayerOffset;

            tooltipInstance = Instantiate(TooltipPrefab, spawnPosition, Quaternion.identity, _WScanvas.transform);
            tooltipInstance.GetComponent<TooltipController>().SetToolTip("T", TooltipText);
            return;
        }

        tooltipInstance = Instantiate(SSTooltipPrefab, _SScanvas.transform);
        tooltipInstance.GetComponent<TooltipController>().SetToolTip("T", TooltipText);
        tooltipInstance.transform.localRotation = Quaternion.identity;
    }
    private void OnTriggerExit(Collider other)
    {
        if(tooltipInstance != null)
        {
            Destroy(tooltipInstance);
        }

    }
    private void Update()
    {
        if (tooltipInstance == null || SpawnTooltipOnSSCanvas)
            return;

        Vector3 directionToPlayer = _playerCamera.position - tooltipInstance.transform.position;
        tooltipInstance.transform.rotation = Quaternion.LookRotation(directionToPlayer);
        tooltipInstance.transform.Rotate(0, 180, 0);
        Debug.Log("Get rotated idiot"); // https://knowyourmeme.com/memes/get-rotated-idiot
    }

    private void Farm(InputAction.CallbackContext context)
    {
        if (tooltipInstance == null)
            return;

        if(HandsController.Instance.EquippedItem?.Name != RequiredItem && RequiredItem != string.Empty)
        {
            DialogSystem.ShowDialog(new Dialog("Info", $"You need to hold {RequiredItem} to do this. If you have {RequiredItem} click on it in the Inventory to equip"), 5);
            return;
        }

        InventoryManager.Instance.AddItem(FarmItem);
        _farmCount++;

        if(IsTree)
        {
            StartCoroutine(gameObject.GetComponent<TreeCut>().EuthanizeSelf());
        }
        
        if(DiscardRequiredItem)
        {
            var playerItem = InventoryManager.Instance.PlayerItems.Where(i => i.Name == RequiredItem).FirstOrDefault();
            HandsController.Instance.Disarm();
            InventoryManager.Instance.RemoveItem(playerItem);
        }
        
        if (OneTimeFarm)
        {
            if (ActivateObject != null)
            {
                ActivateObject.SetActive(true);
            }

            if (!string.IsNullOrEmpty(NewObjective))
            {
                ObjectiveController.Instance.OnObjectiveChangeTrigger(NewObjective);
            }

            Destroy(tooltipInstance);
            Destroy(gameObject);
        }

        if (FarmCountToActivate == 0)
            return;

        if(FarmCountToActivate == _farmCount)
        {
            if (ActivateObject != null)
            {
                ActivateObject.SetActive(true);
            }
        }
    }
}
