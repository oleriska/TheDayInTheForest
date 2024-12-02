using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private string TooltipText;
    [SerializeField]
    private GameObject TooltipPrefab;
    [SerializeField]
    private float TooltipHeight = 0.4f;
    [SerializeField]
    private bool OneTimeUse;
    [SerializeField]
    private string RequiredItem;
    [SerializeField]
    private bool DiscardRequiredItem;
    [SerializeField]
    private GameObject ActivateObject;
    [SerializeField]
    private string NewObjective;
    [SerializeField]
    private int UseCountToActivate;
    [SerializeField]
    private bool DestroyUponUse;

    private Transform _canvas;
    private Transform _playerCamera;
    private InputAction _use;
    private PlayerInput _playerInput;
    private int _useCount;


    private GameObject tooltipInstance;

    private void Start()
    {
        _canvas = GameObject.FindGameObjectWithTag("WSCanvas").GetComponent<Transform>();
        _playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        _use = _playerInput.actions.FindAction("Use");
        _use.performed += Use;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _playerInput.gameObject)
        {
            return;
        }

        Vector3 spawnOffset = new Vector3(0, TooltipHeight, 0);
        Vector3 spawnPosition = gameObject.transform.position + spawnOffset;

        if (tooltipInstance == null)
        {
            tooltipInstance = Instantiate(TooltipPrefab, spawnPosition, Quaternion.identity, _canvas.transform);
            tooltipInstance.GetComponent<TooltipController>().SetToolTip("E", TooltipText);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (tooltipInstance != null)
        {
            Destroy(tooltipInstance);
        }

    }
    private void Update()
    {
        if (tooltipInstance == null)
            return;

        Vector3 directionToPlayer = _playerCamera.position - tooltipInstance.transform.position;
        tooltipInstance.transform.rotation = Quaternion.LookRotation(directionToPlayer);
        tooltipInstance.transform.Rotate(0, 180, 0);
    }

    private void Use(InputAction.CallbackContext context)
    {
        if (tooltipInstance == null)
            return;

        var playerItem = InventoryManager.Instance.PlayerItems.Where(i => i.Name == RequiredItem).FirstOrDefault();

        if (playerItem == null && RequiredItem != string.Empty)
        {
            DialogSystem.ShowDialog(new Dialog("Info", $"You need to have {RequiredItem} to do this."), 5);
            return;
        }

        if (DiscardRequiredItem)
        {
            InventoryManager.Instance.RemoveItem(playerItem);
        }

        _useCount++;

        if (OneTimeUse)
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

        if (UseCountToActivate == 0)
            return;

        if (UseCountToActivate == _useCount)
        {
            if (ActivateObject != null)
            {
                ActivateObject.SetActive(true);
            }

            if (DestroyUponUse)
            {
                Debug.Log("destroying");
                Destroy(tooltipInstance);
                Destroy(this);
            }
        }
    }
}
